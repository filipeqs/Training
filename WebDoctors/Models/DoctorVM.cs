using WebDoctors.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebDoctors.Models
{
    public class DoctorVM
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Specialization")]
        public string SpecializationName { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public ScheduleVM Schedule { get; set; }
        public int ScheduleId { get; set; }
        [Display(Name = "Fee")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public float ConsultationFee { get; set; }
        public string ImagePath { get; set; }
        public PersonVM Person { get; set; }
        public string YoutubeVideo { get; set; }
        public SpecializationVM Specialization { get; set; }
        public string Description { get; set; }
    }

    public class CreateDoctorVM
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public SelectList Specializations { get; set; }
        [Required]
        public int SpecializationId { get; set; }

        [Required]
        [Display(Name = "Consultation Fee")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public float ConsultationFee { get; set; }
        public string YoutubeVideo { get; set; }
    }

    public class EditDoctorVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public SelectList Specializations { get; set; }
        [Required]
        public int SpecializationId { get; set; }

        [Required]
        [Display(Name = "Consultation Fee")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public float ConsultationFee { get; set; }

        public string ImagePath { get; set; }
        public string YoutubeVideo { get; set; }
    }
}
