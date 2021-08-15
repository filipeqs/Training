using System;
using System.ComponentModel.DataAnnotations;

namespace WebDoctors.Data
{
    public class Schedule
    {
        public int Id { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan ConsultationTime { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
    }
}
