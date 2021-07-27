using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTutorial.Models;

namespace MVCTutorial.Controllers
{
    public class LabsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Labs
        public ActionResult Index()
        {
            return View(db.Lab.ToList());
        }

        public ActionResult SelectLabs(int id)
        {
            var patient = db.Patient.SingleOrDefault(p => p.Id == id);
            var patientLabViewModel = new PatientLabViewModel
            {
                Patient = db.Patient.SingleOrDefault(p => p.Id == id),
                Labs = db.Lab.ToList()
            };
            return View(patientLabViewModel);
        }

        [HttpPost]
        public ActionResult SelectLabs(List<Lab> labs)
        {
            string msg = "";
            foreach (var lab in labs)
            {
                if (lab.Check)
                    msg += $"{lab.TestName},";
            }
            msg = msg.TrimEnd(',');

            ViewBag.msg = msg;
            return View(db.Lab.ToList());
        }

        // GET: Labs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Lab.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // GET: Labs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Labs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestName,Check")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                db.Lab.Add(lab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lab);
        }

        // GET: Labs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Lab.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // POST: Labs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestName,Check")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lab);
        }

        // GET: Labs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Lab.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // POST: Labs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lab lab = db.Lab.Find(id);
            db.Lab.Remove(lab);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
