using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Data;

namespace WebDoctors.Models
{
    public class ScheduleTimeVM
    {
        public int Id { get; set; }

        public Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }

        public int DayOfTheWeek { get; set; }
        public string DayOfTheWeekName { get; set; }
        public bool Available { get; set; }

        [DataType(DataType.Time)]
        public string StartTime { get; set; }
        [DataType(DataType.Time)]
        public string EndTime { get; set; }
        public Doctor Doctor{ get; set; }
    }
}
