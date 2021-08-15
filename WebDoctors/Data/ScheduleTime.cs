using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Data
{
    public class ScheduleTime
    {
        public int Id { get; set; }

        public Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }

        public int DayOfTheWeek { get; set; }
        public string DayOfTheWeekName { get; set; }
        public bool Available { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
