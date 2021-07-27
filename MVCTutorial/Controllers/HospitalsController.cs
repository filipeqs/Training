using MVCTutorial.DAL;
using MVCTutorial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTutorial.Controllers
{
    public class HospitalsController : Controller
    {
        readonly CRUD crud = new CRUD();

        // GET: Hospitals
        public ActionResult Index()
        {
            var hospitals = crud.FetchAll();
            return View(hospitals);
        }

        public ActionResult Details(int id)
        {
            var hospital = crud.Fetch(id);
            return View(hospital);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Hospital hospital)
        {
            TempData["msg"] = crud.Create(hospital);
            return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            var hospital = crud.Fetch(id);
            return View(hospital);
        }

        [HttpPost]
        public ActionResult Edit(Hospital hospital)
        {
            TempData["msg"] = crud.Update(hospital);
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var hospital = crud.Fetch(id);
            return View(hospital);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            TempData["msg"] = crud.Delete(id);
            return RedirectToAction("index");
        }
    }
}