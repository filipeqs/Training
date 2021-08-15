using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;
using WebDoctors.Models;

namespace WebDoctors.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Person> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public HomeController(
            IUnitOfWork unitOfWork,
            UserManager<Person> userManager,
            IMapper mapper,
            IEmailSender emailSender
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            var user = isAuthenticated ? await _userManager.GetUserAsync(User) : null;
            IList<Appointment> appointments = isAuthenticated ?
                await _unitOfWork.Appointments
                    .FindAll(q => q.PatientId == user.Id,
                    orderBy: q => q.OrderBy(x => x.AppointmentDay)
                                    .ThenBy(x => x.AppointmentTime))
                : null;

            var appointment = appointments?.FirstOrDefault();
            var specializations = await _unitOfWork.Specializations.FindAll(q => q.Featured);
            var model = new HomeVM
            {
                Specializations = _mapper.Map<List<SpecializationVM>>(specializations),
                IsAuthenticated = isAuthenticated,
                Appointment = _mapper.Map<AppointmentVM>(appointment)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> About(string searchString)
        {
            var doctors = await _unitOfWork.Doctors.FindAll(
                includes: q => q.Include(x => x.Person)
                                .Include(x => x.Specialization));

            if (!string.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(q => 
                    q.Person.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    || q.Person.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    || q.Specialization.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
            }

            var model = _mapper.Map<List<DoctorVM>>(doctors);

            return View(model);
        }

        public async Task<IActionResult> AboutDoctor(int id)
        {
            var doctor = await _unitOfWork.Doctors.Find(
                q => q.Id == id, 
                includes: q => q.Include(x => x.Specialization)
                                .Include(x => x.Person));

            var model = _mapper.Map<DoctorVM>(doctor);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(HomeVM model)
        {
            var specializations = await _unitOfWork.Specializations.FindAll(q => q.Featured);
            model.Specializations = _mapper.Map<List<SpecializationVM>>(specializations);

            if (!ModelState.IsValid)
                return View(nameof(Index), model);

            var email = "filipeqs@outlook.com";

            //await _emailSender.SendEmailAsync(email, model.Subject, model.Message);

            return View(nameof(Index));
        }
    }
}
