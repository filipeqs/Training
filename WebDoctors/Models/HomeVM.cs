using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Models
{
    public class HomeVM
    {
        public List<SpecializationVM> Specializations { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string FromEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsAuthenticated { get; set; }
        public AppointmentVM Appointment { get; set; }
    }
}
