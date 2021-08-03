using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Data
{
    public class Availability
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        public bool Sunday { get; set; }
        public DateTime SundayHours { get; set; }

        public bool Monday { get; set; }
        public DateTime MondayHours { get; set; }

        public bool Tuesday { get; set; }
        public DateTime TuesdayHours { get; set; }

        public bool Wednesday { get; set; }
        public DateTime WednesdayHours { get; set; }

        public bool Thursday { get; set; }
        public DateTime ThursdayHours { get; set; }

        public bool Friday { get; set; }
        public DateTime FridayHours { get; set; }

        public bool Saturday { get; set; }
        public DateTime SaturdayHours { get; set; }
    }
}
