using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Data
{
    public class Doctor
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public string PersonId { get; set; }
        public Specialization Specialization { get; set; }
        [Required]
        public int SpecializationId { get; set; }
        public float ConsultationFee { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string YoutubeVideo { get; set; }
    }
}
