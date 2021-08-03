using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;

namespace WebDoctors.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpecializationsController : Controller
    {
        private readonly ISpecializationRepository _specializationRepository;
        public SpecializationsController(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        // GET: SpecializationsController
        public ActionResult Index()
        {
            var specializations = _specializationRepository.FindAll().ToList();

            return View(specializations);
        }

        // GET: SpecializationsController/Details/5
        public ActionResult Details(int id)
        {
            if (!_specializationRepository.Exists(id))
                return NotFound();

            var specialization = _specializationRepository.FindById(id);

            return View(specialization);
        }

        // GET: SpecializationsController/Create
        public ActionResult Create()
        {
            var specialization = new Specialization();
            return View(specialization);
        }

        // POST: SpecializationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Specialization specialization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(specialization);

                var isSuccess = _specializationRepository.Create(specialization);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(specialization);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(specialization);
            }
        }

        // GET: SpecializationsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_specializationRepository.Exists(id))
                return NotFound();

            var specialization = _specializationRepository.FindById(id);

            return View(specialization);
        }

        // POST: SpecializationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Specialization specialization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                if (!_specializationRepository.Exists(specialization.Id))
                    return NotFound();

                var specializationFromDb = _specializationRepository.FindById(specialization.Id);
                specializationFromDb.Name = specialization.Name;
                specializationFromDb.Description = specialization.Description;

                var isSuccess = _specializationRepository.Update(specializationFromDb);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(specialization);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpecializationsController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_specializationRepository.Exists(id))
                return NotFound();

            var specialization = _specializationRepository.FindById(id);

            return View(specialization);
        }

        // POST: SpecializationsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAction(int id)
        {
            try
            {
                if (!_specializationRepository.Exists(id))
                    return NotFound();

                var specialization = _specializationRepository.FindById(id);

                var isSuccess = _specializationRepository.Delete(specialization);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(specialization);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
