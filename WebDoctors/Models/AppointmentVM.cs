using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Data;

namespace WebDoctors.Models
{
    public class AppointmentVM
    {
        public int Id { get; set; }

        public Person Patient { get; set; }
        public string PatientId { get; set; }

        public DoctorVM Doctor { get; set; }
        public int DoctorId { get; set; }

        [Display(Name = "Appointment Day")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDay { get; set; }

        [Display(Name = "Appointment Time")]
        public TimeSpan AppointmentTime { get; set; }

        public int AppointmentType { get; set; }
        [Display(Name = "Type")]
        public string AppointmentTypeName { get; set; }

        public string DiagnosisFilePath { get; set; }

        public string PrescriptionFilePath { get; set; }

        public string TestResultsFilePath { get; set; }

        public string DietPlanFilePath { get; set; }
        public bool Cancelled { get; set; }
        public bool Completed { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }
        public bool IsPaid { get; set; }
    }

    public class MyAppointmentVM
    {
        public Person Patient { get; set; }
        public string PatientId { get; set; }

        public List<AppointmentVM> Appointments { get; set; }
    }

    public class CreateAppointmentVM
    {
        public string PatientId { get; set; }

        public List<SelectListItem> Doctors { get; set; }
        public int DoctorId { get; set; }

        public List<SelectListItem> Specialization { get; set; }
        public int SpecializationId { get; set; }

        public List<SelectListItem> AppointmentTypes { get; set; }
        public int AppointmentType { get; set; }

        [Display(Name = "Select A Day")]
        public string AppointmentDay { get; set; }

        public List<SelectListItem> AppointmentTimes { get; set; }
        public string AppointmentTimeSelected { get; set; }
        public float ConsultationFee { get; set; }
    }

    public class EditAppointmentVM
    {
        public int Id { get; set; }

        public PersonVM Patient { get; set; }
        public string PatientId { get; set; }
        public DoctorVM Doctor { get; set; }
        public int DoctorId { get; set; }

        public List<SelectListItem> AppointmentTypes { get; set; }
        public int AppointmentType { get; set; }

        [Display(Name = "Select A Day")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDay { get; set; }

        public float Price { get; set; }

        public List<SelectListItem> AppointmentTimes { get; set; }
        [Display(Name = "Appointment Time")]
        public string AppointmentTimeSelected { get; set; }
        public string DiagnosisFilePath { get; set; }
        public string PrescriptionFilePath { get; set; }
        public string TestResultsFilePath { get; set; }
        public string DietPlanFilePath { get; set; }
    }
}
