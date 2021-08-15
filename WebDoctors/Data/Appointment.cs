using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Data
{
    public class Appointment
    {
        public int Id { get; set; }

        public Person Patient { get; set; }
        [ForeignKey("Person")]
        public string PatientId { get; set; }

        public Doctor Doctor { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public DateTime AppointmentDay { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        public int AppointmentType { get; set; }

        public string DiagnosisFilePath { get; set; }

        public string PrescriptionFilePath  { get; set; }

        public string TestResultsFilePath { get; set; }

        public string DietPlanFilePath { get; set; }

        public bool Cancelled { get; set; }
        public bool Completed { get; set; }
        public float Price { get; set; }
        public bool IsPaid { get; set; }
        public string TransactionId { get; set; }
    }
}
