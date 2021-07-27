using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class PatientLabViewModel
    {
        public List<Lab> Labs { get; set; }
        public Patient Patient { get; set; }
    }
}