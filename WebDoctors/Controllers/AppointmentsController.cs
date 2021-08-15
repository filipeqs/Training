using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;
using WebDoctors.Enums;
using WebDoctors.Models;

namespace WebDoctors.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<WebDoctors.Data.Person> _userManager;
        private readonly IFileUpload _fileUpload;

        public AppointmentsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<WebDoctors.Data.Person> userManager,
            IFileUpload fileUpload
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _fileUpload = fileUpload;
        }

        [Authorize(Roles = "Admin, Doctor")]
        public async Task<ActionResult> Index()
        {
            var appointments = await _unitOfWork.Appointments.FindAll(
                includes: q => q.Include(x => x.Doctor)
                                .Include(x => x.Doctor.Person)
                                .Include(x => x.Patient));
            var doctors = await _unitOfWork.Doctors.FindAll();

            var model = _mapper.Map<List<AppointmentVM>>(appointments);

            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> MyAppointments()
        {
            var patient = await _userManager.GetUserAsync(User);
            var appointments = await _unitOfWork.Appointments.FindAll(
                q => q.PatientId == patient.Id, 
                includes: q => q.Include(x => x.Doctor)
                                .Include(x => x.Doctor.Person)
                                .Include(x => x.Patient));

            var appointmentsModel = _mapper.Map<List<AppointmentVM>>(appointments);
            var doctors = await _unitOfWork.Doctors.FindAll(includes: q => q.Include(x => x.Person));

            var model = new MyAppointmentVM
            {
                Patient = patient,
                PatientId = patient.Id,
                Appointments = appointmentsModel
            };

            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                return NotFound();
                        
            var appointment = await _unitOfWork.Appointments.Find(
                q => q.Id == id, 
                includes: q => q.Include(x => x.Patient)
                                .Include(x => x.Doctor)
                                .Include(x => x.Doctor.Person));
            var user = await _userManager.GetUserAsync(User);

            if (!IsAdminOrDoctor())
            {
                if (user.Id != appointment.PatientId)
                    return NotFound();
            }

            var model = _mapper.Map<AppointmentVM>(appointment);
            model.AppointmentTypeName = ((AppointmentTypes)appointment.AppointmentType).ToString();

            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Create(string id)
        {
            var patient = await _userManager.FindByIdAsync(id);
            if (patient == null)
                return NotFound();

            var specializations = await _unitOfWork.Specializations.FindAll();
            var model = new CreateAppointmentVM
            {
                PatientId = id,
                Specialization = specializations.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
                AppointmentTypes = GetAppointmentTypesSelectList()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAppointmentVM model)
        {
            try
            {
                await PrepareCreateAppointmentModel(model);

                if (!ModelState.IsValid)
                    return View(model);

                if (model.AppointmentType == 0)
                {
                    ModelState.AddModelError("", "Please Select A Type");
                    return View(model);
                }

                var doctor = await _unitOfWork.Doctors.Find(q => q.Id == model.DoctorId);

                var appointment = new Appointment
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    AppointmentDay = Convert.ToDateTime(model.AppointmentDay),
                    AppointmentTime = TimeSpan.Parse(model.AppointmentTimeSelected),
                    AppointmentType = model.AppointmentType,
                    Price = doctor.ConsultationFee
                };
                await _unitOfWork.Appointments.Create(appointment);
                await _unitOfWork.Save();

                if (IsAdminOrDoctor())
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(MyAppointments));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
        }

        [Authorize]
        public async Task<ActionResult> GetDoctorsBySpecialization(int id)
        {
            try
            {
                var doctors = await _unitOfWork.Doctors.FindAll(q => 
                    q.SpecializationId == id, 
                    includes: q => q.Include(x => x.Person));

                return Json(doctors);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        [Authorize]
        public async Task<ActionResult> GetDoctorScheduleTime(int id)
        {
            try
            {
                if (!(await _unitOfWork.Doctors.Exists(q => q.Id == id)))
                    return Json(null);

                var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == id);
                var scheduleTime = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);
                var model = _mapper.Map<List<ScheduleTimeVM>>(scheduleTime);

                return Json(model);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        [HttpGet("/Appointments/GetAvailableTimes/{doctorId}/{date}")]
        [Authorize]
        public async Task<ActionResult> GetAvailableTimes(int doctorId, DateTime date)
        {
            try
            {
                if (!(await _unitOfWork.Doctors.Exists(q => q.Id == doctorId)))
                    return Json(null);

                var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == doctorId);
                var scheduleTime = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);

                var selectedDayOfTheWeek = (int)date.DayOfWeek;
                var selectedSchedule = scheduleTime.SingleOrDefault(s => s.DayOfTheWeek == selectedDayOfTheWeek);

                if (!selectedSchedule.Available)
                    return Json(null);

                var appointments = await _unitOfWork.Appointments.FindAll(q => q.DoctorId == doctorId && q.AppointmentDay == date.Date);

                var startTime = Convert.ToDateTime(selectedSchedule.StartTime);
                var endTime = Convert.ToDateTime(selectedSchedule.EndTime);
                var times = new List<SelectListItem>();

                for (var time = startTime; time.CompareTo(endTime) < 0; time = time.Add(schedule.ConsultationTime))
                {
                    if (appointments.Any(a => a.AppointmentTime.ToString() == time.ToString("HH:mm:ss") && !a.Cancelled))
                        continue;
  
                    times.Add(new SelectListItem
                    {
                        Value = time.ToString("HH:mm"),
                        Text = time.ToString("hh:mm tt", CultureInfo.CurrentCulture)
                    });
                }

                return Json(times);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        // GET: AppointmentsController/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                return NotFound();

            var appointment = await _unitOfWork.Appointments.Find(q => q.Id == id);
            var patient = await _userManager.GetUserAsync(User);
            if (!IsAdminOrDoctor())
            {
                if (patient.Id != appointment.PatientId)
                    return NotFound();
            }

            var doctor = await _unitOfWork.Doctors.Find(
                q => q.Id == appointment.DoctorId,
                includes: q => q.Include(x => x.Person)
                                .Include(x => x.Specialization));
            var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == doctor.Id);
            var scheduleTime = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);
            var types = Enum.GetValues(typeof(AppointmentTypes));

            var doctorModel = new DoctorVM
            {
                Id = doctor.Id,
                FirstName = doctor.Person.FirstName,
                LastName = doctor.Person.LastName,
                SpecializationName = doctor.Specialization.Name,
                DateOfBirth = doctor.Person.DateOfBirth,
                Schedule = _mapper.Map<ScheduleVM>(schedule),
                ScheduleId = schedule.Id,
                ConsultationFee = doctor.ConsultationFee
            };

            var model = new EditAppointmentVM
            {
                Id = appointment.Id,
                Patient = _mapper.Map<PersonVM>(patient),
                PatientId = appointment.PatientId,
                Doctor = doctorModel,
                DoctorId = doctor.Id,
                AppointmentType = appointment.AppointmentType,
                AppointmentDay = appointment.AppointmentDay,
                AppointmentTimeSelected = string.Format("{0:00}:{1:00}",  appointment.AppointmentTime.Hours, appointment.AppointmentTime.Minutes),
                Price = appointment.Price,
                DiagnosisFilePath = appointment.DiagnosisFilePath,
                PrescriptionFilePath = appointment.PrescriptionFilePath,
                TestResultsFilePath = appointment.TestResultsFilePath,
                DietPlanFilePath = appointment.DietPlanFilePath
            };

            model.AppointmentTypes = new List<SelectListItem>();
            foreach (var type in types)
            {
                model.AppointmentTypes.Add(new SelectListItem
                {
                    Text = type.ToString(),
                    Value = ((int)type).ToString()
                });
            }

            var appointments = await _unitOfWork.Appointments.FindAll(q => q.DoctorId == doctor.Id && q.AppointmentDay == appointment.AppointmentDay);
            var selectedSchedule = scheduleTime.SingleOrDefault(s => s.DayOfTheWeek == (int)appointment.AppointmentDay.DayOfWeek);
            var startTime = Convert.ToDateTime(selectedSchedule.StartTime);
            var endTime = Convert.ToDateTime(selectedSchedule.EndTime);
            var times = new List<SelectListItem>();

            for (var time = startTime; time.CompareTo(endTime) < 0; time = time.Add(schedule.ConsultationTime))
            {
                if (appointments.Any(a => 
                    a.AppointmentTime.ToString() == time.ToString("HH:mm:ss") 
                    && !a.Cancelled
                    && a.Id != appointment.Id))
                    continue;

                times.Add(new SelectListItem
                {
                    Value = time.ToString("HH:mm"),
                    Text = time.ToString("hh:mm tt", CultureInfo.CurrentCulture)
                });
            }
            model.AppointmentTimes = times;

            return View(model);
        }

        // POST: AppointmentsController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            EditAppointmentVM model, 
            IFormFile diagnosisFile, 
            IFormFile prescriptionFile,
            IFormFile testResultsFile,
            IFormFile dietPlanFile)
        {
            try
            {
                var appointment = await _unitOfWork.Appointments.Find(q => q.Id == model.Id);
                var patient = await _userManager.GetUserAsync(User);
                if (!(User.IsInRole("Doctor") || User.IsInRole("Admin")))
                {
                    if (patient.Id != appointment.PatientId)
                        return NotFound();
                }

                var doctor = await _unitOfWork.Doctors.Find(
                           q => q.Id == appointment.DoctorId,
                           includes: q => q.Include(x => x.Person)
                                           .Include(x => x.Specialization));
                var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == doctor.Id);
                var scheduleTime = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);
                var types = Enum.GetValues(typeof(AppointmentTypes));

                var doctorModel = new DoctorVM
                {
                    Id = doctor.Id,
                    FirstName = doctor.Person.FirstName,
                    LastName = doctor.Person.LastName,
                    SpecializationName = doctor.Specialization.Name,
                    DateOfBirth = doctor.Person.DateOfBirth,
                    Schedule = _mapper.Map<ScheduleVM>(schedule),
                    ScheduleId = schedule.Id,
                    ConsultationFee = doctor.ConsultationFee,
                    ImagePath = doctor.ImagePath
                };

                model.Patient = _mapper.Map<PersonVM>(patient);
                model.Doctor = doctorModel;

                model.AppointmentTypes = new List<SelectListItem>();
                foreach (var type in types)
                {
                    model.AppointmentTypes.Add(new SelectListItem
                    {
                        Text = type.ToString(),
                        Value = ((int)type).ToString()
                    });
                }

                var appointments = await _unitOfWork.Appointments.FindAll(q => q.DoctorId == doctor.Id && q.AppointmentDay == appointment.AppointmentDay);
                var selectedSchedule = scheduleTime.SingleOrDefault(s => s.DayOfTheWeek == (int)appointment.AppointmentDay.DayOfWeek);
                var startTime = Convert.ToDateTime(selectedSchedule.StartTime);
                var endTime = Convert.ToDateTime(selectedSchedule.EndTime);
                var times = new List<SelectListItem>();

                for (var time = startTime; time.CompareTo(endTime) < 0; time = time.Add(schedule.ConsultationTime))
                {
                    if (appointments.Any(a => a.AppointmentTime.ToString() == time.ToString("HH:mm:ss") && !a.Cancelled))
                        continue;

                    times.Add(new SelectListItem
                    {
                        Value = time.ToString("HH:mm"),
                        Text = time.ToString("hh:mm tt", CultureInfo.CurrentCulture)
                    });
                }
                model.AppointmentTimes = times;

                if (!ModelState.IsValid)
                    return View(model);

                appointment.AppointmentType = model.AppointmentType;
                appointment.AppointmentDay = Convert.ToDateTime(model.AppointmentDay);
                appointment.AppointmentTime = TimeSpan.Parse(model.AppointmentTimeSelected);
                appointment.Price = model.Price;

                bool isDiagnosisUploaded = _fileUpload.SaveFile(diagnosisFile);
                if (isDiagnosisUploaded)
                    appointment.DiagnosisFilePath = _fileUpload.GetFilePath(diagnosisFile);

                bool isPrescriptionUploaded = _fileUpload.SaveFile(prescriptionFile);
                if (isPrescriptionUploaded)
                    appointment.PrescriptionFilePath = _fileUpload.GetFilePath(prescriptionFile);

                bool isTestResultsUploaded = _fileUpload.SaveFile(testResultsFile);
                if (isTestResultsUploaded)
                    appointment.TestResultsFilePath = _fileUpload.GetFilePath(testResultsFile);

                bool isDietPlanUploaded = _fileUpload.SaveFile(dietPlanFile);
                if (isDietPlanUploaded)
                    appointment.DietPlanFilePath = _fileUpload.GetFilePath(dietPlanFile);

                _unitOfWork.Appointments.Update(appointment);
                await _unitOfWork.Save();

                if (User.IsInRole("Admin") || User.IsInRole("Doctor"))
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(MyAppointments));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: AppointmentsController/Cancel/5
        [Authorize]
        public async Task<ActionResult> Cancel(int id)
        {
            if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                return NotFound();

            var appointment = await _unitOfWork.Appointments.Find(
                q => q.Id == id, 
                includes: q => q.Include(x => x.Doctor)
                                .Include(x => x.Doctor.Person)
                                .Include(x => x.Patient));

            var patient = await _userManager.GetUserAsync(User);
            if (!(User.IsInRole("Doctor") || User.IsInRole("Admin")))
            {
                if (patient.Id != appointment.PatientId)
                    return NotFound();
            }

            var model = _mapper.Map<AppointmentVM>(appointment);
            model.AppointmentTypeName = ((AppointmentTypes)appointment.AppointmentType).ToString();

            return View(model);
        }

        // POST: AppointmentsController/Delete/5
        [HttpPost]
        [ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CancelAppointment(int id)
        {
            try
            {
                if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                    return NotFound();

                var appointment = await _unitOfWork.Appointments.Find(q => q.Id == id);
                var patient = await _userManager.GetUserAsync(User);
                if (!(User.IsInRole("Doctor") || User.IsInRole("Admin")))
                {
                    if (patient.Id != appointment.PatientId)
                        return NotFound();
                }

                appointment.Cancelled = true;

                _unitOfWork.Appointments.Update(appointment);
                await _unitOfWork.Save();

                if (User.IsInRole("Admin") || User.IsInRole("Doctor"))
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(MyAppointments));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin, Doctor")]
        public async Task<ActionResult> Complete(int id)
        {
            if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                return NotFound();

            var appointment = await _unitOfWork.Appointments.Find(
                q => q.Id == id,
                includes: q => q.Include(x => x.Doctor)
                                .Include(x => x.Doctor.Person)
                                .Include(x => x.Patient));
            var model = _mapper.Map<AppointmentVM>(appointment);
            //model.Doctor.Person = appointment.Doctor.Person;
            model.AppointmentTypeName = ((AppointmentTypes)appointment.AppointmentType).ToString();

            return View(model);
        }

        [HttpPost]
        [ActionName("Complete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<ActionResult> CompleteAppointment(int id)
        {
            try
            {
                if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                    return NotFound();

                var appointment = await _unitOfWork.Appointments.Find(q => q.Id == id);
                appointment.Completed = true;

                _unitOfWork.Appointments.Update(appointment);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Payment(int id, string stripeEmail, string stripeToken)
        {
            if (!(await _unitOfWork.Appointments.Exists(q => q.Id == id)))
                return NotFound();

            var appointment = await _unitOfWork.Appointments.Find(q => q.Id == id);
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (long)(appointment.Price * 100),
                Currency = "USD",
                Description = $"Payment for Appointment: {appointment.Id}",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>
                {
                    { "AppointmentId", appointment.Id.ToString() },
                }
            });

            if (charge.Status == "succeeded")
            {
                appointment.IsPaid = true;
                appointment.TransactionId = charge.BalanceTransactionId;
                _unitOfWork.Appointments.Update(appointment);
                await _unitOfWork.Save();

                TempData["PaymentSuccess"] = "Payment Successful";
                return RedirectToAction(nameof(Details), new
                {
                    id = appointment.Id,
                }); ;
            }
            else
            {
                TempData["PaymentError"] = "Payment Error";
                return RedirectToAction(nameof(Details), new
                {
                    id = appointment.Id,
                }); ;
            }
        }

        private bool IsAdminOrDoctor() =>
            User.IsInRole("Doctor") || User.IsInRole("Admin");

        private List<SelectListItem> GetAppointmentTypesSelectList()
        {
            var types = Enum.GetValues(typeof(AppointmentTypes));
            var appointmentTypes = new List<SelectListItem>();
            foreach (var type in types)
            {
                appointmentTypes.Add(new SelectListItem
                {
                    Text = type.ToString(),
                    Value = ((int)type).ToString()
                });
            }

            return appointmentTypes;
        }

        private async Task PrepareCreateAppointmentModel(CreateAppointmentVM model)
        {
            var specializations = await _unitOfWork.Specializations.FindAll();
            model.Specialization = specializations.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList();
            model.AppointmentTypes = GetAppointmentTypesSelectList();
        }
    }
}
