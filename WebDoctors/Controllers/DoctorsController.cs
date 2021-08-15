using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;
using WebDoctors.Models;

namespace WebDoctors.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Person> _userManager;
        private readonly IMapper _mapper;
        private readonly IImageUpload _imageUpload;

        public DoctorsController(
            IUnitOfWork unitOfWork,
            UserManager<Person> manager,
            IMapper mapper,
            IImageUpload imageUpload
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = manager;
            _mapper = mapper;
            _imageUpload = imageUpload;
        }

        // GET: DoctorsController
        public async Task<ActionResult> Index()
        {
            var doctors = await _unitOfWork.Doctors.FindAll(
                    includes: q => q.Include(x => x.Person)
                                    .Include(x => x.Specialization));
            var schedules = await _unitOfWork.Schedules.FindAll();
            var model = new List<DoctorVM>();
            foreach (var doctor in doctors)
            {
                var schedule = schedules.FirstOrDefault(a => a.DoctorId == doctor.Id);
                var newDoctor = new DoctorVM
                {
                    Id = doctor.Id,
                    FirstName = doctor.Person.FirstName,
                    LastName = doctor.Person.LastName,
                    SpecializationName = doctor.Specialization.Name,
                    ScheduleId = schedule.Id
                };

                model.Add(newDoctor);
            }

            return View(model);
        }

        // GET: DoctorsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (!(await _unitOfWork.Doctors.Exists(q => q.Id == id)))
                return NotFound();

            var doctor = await _unitOfWork.Doctors.Find(
                q => q.Id == id,
                includes: q => q.Include(x => x.Person)
                                .Include(x => x.Specialization));
            var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == id);
            var model = new DoctorVM
            {
                Id = doctor.Id,
                FirstName = doctor.Person.FirstName,
                LastName = doctor.Person.LastName,
                SpecializationName = doctor.Specialization.Name,
                DateOfBirth = doctor.Person.DateOfBirth,
                ScheduleId = schedule.Id,
                Schedule = _mapper.Map<ScheduleVM>(schedule),
                ConsultationFee = doctor.ConsultationFee,
                ImagePath = doctor.ImagePath,
                YoutubeVideo = doctor.YoutubeVideo
            };

            return View(model);
        }

        // GET: DoctorsController/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateDoctorVM();
            model.DateOfBirth = DateTime.Now;
            var specializaions = await _unitOfWork.Specializations.FindAll();
            model.Specializations = new SelectList(specializaions, "Id", "Name");

            return View(model);
        }

        // POST: DoctorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDoctorVM model, IFormFile image)
        {
            try
            {
                var specializaions = await _unitOfWork.Specializations.FindAll();
                model.Specializations = new SelectList(specializaions, "Id", "Name");

                if (!ModelState.IsValid)
                    return View(model);

                var user = new Person
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }

                await _userManager.AddToRoleAsync(user, "Doctor");

                var doctor = new Doctor 
                { 
                    PersonId = user.Id,
                    SpecializationId = model.SpecializationId,
                    ConsultationFee = model.ConsultationFee,
                    YoutubeVideo = model.YoutubeVideo
                };
                var isUploaded = _imageUpload.SaveFile(image);
                if (isUploaded)
                    doctor.ImagePath = _imageUpload.GetFilePath(image);

                await _unitOfWork.Doctors.Create(doctor);
                await _unitOfWork.Save();

                var schedule = new Schedule { DoctorId = doctor.Id };
                await _unitOfWork.Schedules.Create(schedule);
                await _unitOfWork.Save();

                var daysOfTheWeek = new List<DayOfWeek>();
                daysOfTheWeek.Add(DayOfWeek.Sunday);
                daysOfTheWeek.Add(DayOfWeek.Monday);
                daysOfTheWeek.Add(DayOfWeek.Tuesday);
                daysOfTheWeek.Add(DayOfWeek.Wednesday);
                daysOfTheWeek.Add(DayOfWeek.Thursday);
                daysOfTheWeek.Add(DayOfWeek.Friday);
                daysOfTheWeek.Add(DayOfWeek.Saturday);
                foreach (var day in daysOfTheWeek)
                {
                    var dayToAdd = new ScheduleTime
                    {
                        ScheduleId = schedule.Id,
                        DayOfTheWeek = (int)day,
                        DayOfTheWeekName = day.ToString()
                    };
                    await _unitOfWork.ScheduleTimes.Create(dayToAdd);
                }
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: DoctorsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (!(await _unitOfWork.Doctors.Exists(q => q.Id == id)))
                return NotFound();

            var doctor = await _unitOfWork.Doctors.Find(
                q => q.Id == id,
                includes: q => q.Include(x => x.Person));
            var specializations = await _unitOfWork.Specializations.FindAll();

            var model = new EditDoctorVM
            {
                Id = doctor.Id,
                FirstName = doctor.Person.FirstName,
                LastName = doctor.Person.LastName,
                DateOfBirth = doctor.Person.DateOfBirth,
                SpecializationId = doctor.SpecializationId,
                ConsultationFee = doctor.ConsultationFee,
                ImagePath = doctor.ImagePath,
                YoutubeVideo = doctor.YoutubeVideo
            };
            model.Specializations = new SelectList(specializations, "Id", "Name");

            return View(model);
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditDoctorVM model, IFormFile image)
        {
            try
            {
                var specializaions = await _unitOfWork.Specializations.FindAll();
                model.Specializations = new SelectList(specializaions, "Id", "Name");

                if (!ModelState.IsValid)
                    return View(model);

                var doctor = await _unitOfWork.Doctors.Find(
                    q => q.Id == model.Id,
                    includes: q => q.Include(x => x.Person));
                var person = await _userManager.FindByIdAsync(doctor.Person.Id);

                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                person.DateOfBirth = model.DateOfBirth;
                var result = await _userManager.UpdateAsync(person);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }

                var isUploaded = _imageUpload.SaveFile(image);
                if (isUploaded)
                    doctor.ImagePath = _imageUpload.GetFilePath(image);

                doctor.SpecializationId = model.SpecializationId;
                doctor.ConsultationFee = model.ConsultationFee;
                doctor.YoutubeVideo = model.YoutubeVideo;

                _unitOfWork.Doctors.Update(doctor);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: DoctorsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (!(await _unitOfWork.Doctors.Exists(q => q.Id == id)))
                return NotFound();

            var doctor = await _unitOfWork.Doctors.Find(
                q => q.Id == id,
                includes: q => q.Include(x => x.Person));

            var model = new DoctorVM
            {
                Id = doctor.Id,
                FirstName = doctor.Person.FirstName,
                LastName = doctor.Person.LastName,
                DateOfBirth = doctor.Person.DateOfBirth
            };

            return View(model);
        }

        // POST: DoctorsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDoctor(DoctorVM model)
        {
            try
            {
                if (!(await _unitOfWork.Doctors.Exists(q => q.Id == model.Id)))
                    return NotFound();

                var doctor = await _unitOfWork.Doctors.Find(
                    q => q.Id == model.Id,
                    includes: q => q.Include(x => x.Person));
                var user = await _userManager.FindByIdAsync(doctor.Person.Id);

                _unitOfWork.Doctors.Delete(doctor);
                await _unitOfWork.Save();

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }
    }
}
