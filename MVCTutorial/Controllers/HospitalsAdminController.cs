using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVCTutorial.Logger;
using MVCTutorial.Models;

namespace MVCTutorial.Controllers
{
    [Authorize(Roles ="admin")]
    public class HospitalsAdminController : Controller
    {
        private HospitalContext db = new HospitalContext();

        private void SaveLog(string page)
        {
            var user = HttpContext.User.Identity;
            var log = new Log()
            {
                UserId = user.GetUserId(),
                UserName = user.Name,
                DateAccess = DateTime.Now,
                Page = page
            };

            var userLogger = new UserLogger();
            userLogger.SaveLog(log);
        }

        public ActionResult Reports()
        {
            // Viewmodel and Dynamic
            var patients = db.Patient.ToList();
            var doctors = db.Doctor.ToList();
            var labs = db.Lab.ToList();
            ViewData["Hospitals"] = db.Hospital.ToList();

            dynamic d = new ExpandoObject();
            d.Patients = patients;
            d.Doctors = doctors;
            d.Labs = labs;

            return View(d);
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
