using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCTutorial.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        [Display(Name = "NAME")]
        public string Name { get; set; }
        [Display(Name = "ADDRESS")]
        public string Address { get; set; }
        [Display(Name = "MOBILE")]
        public string Mobile { get; set; }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Ailment { get; set; }
        public string Email { get; set; }
        [ForeignKey("Hospital")]
        [Display(Name = "Hospital")]
        public int? HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public List<Lab> Labs { get; set; }
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        [ForeignKey("Hospital")]
        [Display(Name = "Hospital")]
        public int? HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }

    public class Lab 
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public bool Check { get; set; }
        public List<Patient> Patients { get; set; }
    }


    public class HospitalContext : DbContext
    {
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Lab> Lab { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}