using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Data
{
    public class Appointment
    {
        public int Id { get; set; }

        public Person Person { get; set; }
        public int PersonId { get; set; }

        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string DiagnosisFilePath { get; set; }

        public string PrescriptionFilePath  { get; set; }

        public string TestResultsFilePath { get; set; }

        public string DietPlanFilePath { get; set; }
    }
}
