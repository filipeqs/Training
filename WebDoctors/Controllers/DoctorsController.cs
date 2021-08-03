using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IDoctorRepository _doctorRepository ;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly UserManager<Person> _userManager;
        public DoctorsController(
            IDoctorRepository doctorRepository, 
            ISpecializationRepository specializationRepository,
            UserManager<Person> manager
            )
        {
            _doctorRepository = doctorRepository;
            _specializationRepository = specializationRepository;
            _userManager = manager;
        }

        // GET: DoctorsController
        public ActionResult Index()
        {
            var users = _doctorRepository.FindAll();
            var model = new List<DoctorVM>();
            foreach (var user in users)
            {
                var newDoctor = new DoctorVM
                {
                    Id = user.Id,
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                    SpecializationName = user.Specialization.Name
                };

                model.Add(newDoctor);
            }

            return View(model);
        }

        // GET: DoctorsController/Details/5
        public ActionResult Details(int id)
        {
            if (!_doctorRepository.Exists(id))
                return NotFound();

            var doctor = _doctorRepository.FindById(id);
            var model = new DoctorVM
            {
                Id = doctor.Id,
                FirstName = doctor.Person.FirstName,
                LastName = doctor.Person.LastName,
                SpecializationName = doctor.Specialization.Name,
                DateOfBirth = doctor.Person.DateOfBirth
            };

            return View(model);
        }

        // GET: DoctorsController/Create
        public ActionResult Create()
        {
            var model = new CreateDoctorVM();
            var specializaions = _specializationRepository.FindAll();
            model.Specializations = new SelectList(specializaions, "Id", "Name");

            return View(model);
        }

        // POST: DoctorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDoctorVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return NotFound();

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
                    SpecializationId = model.SpecializationId
                };
                var isSuccess = _doctorRepository.Create(doctor);
                if (!isSuccess)
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

        // GET: DoctorsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_doctorRepository.Exists(id))
                return NotFound();

            var doctor = _doctorRepository.FindById(id);
            var specializaions = _specializationRepository.FindAll();

            var model = new EditDoctorVM
            {
                Id = doctor.Id,
                FirstName = doctor.Person.FirstName,
                LastName = doctor.Person.LastName,
                DateOfBirth = doctor.Person.DateOfBirth,
                SpecializationId = doctor.SpecializationId
            };
            model.Specializations = new SelectList(specializaions, "Id", "Name");

            return View(model);
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditDoctorVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var specializaions = _specializationRepository.FindAll();
                    model.Specializations = new SelectList(specializaions, "Id", "Name");
                    return View(model);
                }

                var doctor = _doctorRepository.FindById(model.Id);
                var person = await _userManager.FindByIdAsync(doctor.Person.Id);

                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                person.DateOfBirth = model.DateOfBirth;
                var result = await _userManager.UpdateAsync(person);
                if (!result.Succeeded)
                {
                    var specializaions = _specializationRepository.FindAll();
                    model.Specializations = new SelectList(specializaions, "Id", "Name");
                    return View(model);
                }

                doctor.SpecializationId = model.SpecializationId;
                var isSuccess = _doctorRepository.Update(doctor);
                if (!isSuccess)
                {
                    var specializaions = _specializationRepository.FindAll();
                    model.Specializations = new SelectList(specializaions, "Id", "Name");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
