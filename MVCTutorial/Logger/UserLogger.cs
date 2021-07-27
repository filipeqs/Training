using Microsoft.AspNet.Identity;
using MVCTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MVCTutorial.Logger
{
    public class UserLogger
    {
        private HospitalContext db = new HospitalContext();

        public void SaveLog(Log log)
        {
            db.Log.Add(log);
            db.SaveChanges();
        }
    }
}