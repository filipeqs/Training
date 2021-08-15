using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebDoctors.Data;

namespace WebDoctors.Models
{
    public class ScheduleVM
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        [Display(Name = "Consultation Time")]
        public string ConsultationTime { get; set; }

        public bool Sunday { get; set; }

        [DataType(DataType.Time)]
        public string SundayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string SundayEndTime { get; set; }

        public bool Monday { get; set; }

        [DataType(DataType.Time)]
        public string MondayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string MondayEndTime { get; set; }

        public bool Tuesday { get; set; }

        [DataType(DataType.Time)]
        public string TuesdayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string TuesdayEndTime { get; set; }

        public bool Wednesday { get; set; }

        [DataType(DataType.Time)]
        public string WednesdayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string WednesdayEndTime { get; set; }

        public bool Thursday { get; set; }

        [DataType(DataType.Time)]
        public string ThursdayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string ThursdayEndTime { get; set; }

        public bool Friday { get; set; }

        [DataType(DataType.Time)]
        public string FridayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string FridayEndTime { get; set; }

        public bool Saturday { get; set; }

        [DataType(DataType.Time)]
        public string SaturdayStartTime { get; set; }

        [DataType(DataType.Time)]
        public string SaturdayEndTime { get; set; }
    }
}
