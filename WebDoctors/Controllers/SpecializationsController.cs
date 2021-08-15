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
    [Authorize(Roles = "Admin, Doctor")]
    public class SpecializationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SpecializationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: SpecializationsController
        public async Task<ActionResult> Index()
        {
            var specializations = await _unitOfWork.Specializations.FindAll();

            return View(specializations);
        }

        // GET: SpecializationsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (!(await _unitOfWork.Specializations.Exists(q => q.Id == id)))
                return NotFound();

            var specialization = await _unitOfWork.Specializations.Find(q => q.Id == id);

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
        public async Task<ActionResult> Create(Specialization specialization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(specialization);

                await _unitOfWork.Specializations.Create(specialization);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(specialization);
            }
        }

        // GET: SpecializationsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (!(await _unitOfWork.Specializations.Exists(q => q.Id == id)))
                return NotFound();

            var specialization = await _unitOfWork.Specializations.Find(q => q.Id == id);

            return View(specialization);
        }

        // POST: SpecializationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Specialization specialization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                if (!(await _unitOfWork.Specializations.Exists(q => q.Id == specialization.Id)))
                    return NotFound();

                var specializationFromDb = await _unitOfWork.Specializations.Find(q => q.Id == specialization.Id);
                specializationFromDb.Name = specialization.Name;
                specializationFromDb.Description = specialization.Description;
                specializationFromDb.Featured = specialization.Featured;

                _unitOfWork.Specializations.Update(specializationFromDb);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpecializationsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (!(await _unitOfWork.Specializations.Exists(q => q.Id == id)))
                return NotFound();

            var specialization = await _unitOfWork.Specializations.Find(q => q.Id == id);

            return View(specialization);
        }

        // POST: SpecializationsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAction(int id)
        {
            try
            {
                if (!(await _unitOfWork.Specializations.Exists(q => q.Id == id)))
                    return NotFound();

                var specialization = await _unitOfWork.Specializations.Find(q => q.Id == id);

                _unitOfWork.Specializations.Delete(specialization);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
