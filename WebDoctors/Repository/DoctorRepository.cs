using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;

namespace WebDoctors.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _db;
        public DoctorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Doctor entity)
        {
            _db.Doctors.Add(entity);
            return Save();
        }

        public bool Delete(Doctor entity)
        {
            _db.Doctors.Remove(entity);
            return Save();
        }

        public bool Exists(int id) =>
            _db.Doctors.Any(d => d.Id == id);

        public ICollection<Doctor> FindAll() =>
            _db.Doctors
            .Include(d => d.Person)
            .Include(d => d.Specialization)
            .ToList();

        public Doctor FindById(int id) =>
            _db.Doctors
            .Include(d => d.Person)
            .Include(d => d.Specialization)
            .SingleOrDefault(d => d.Id == id);

        public bool Save() =>
            _db.SaveChanges() > 0;

        public bool Update(Doctor entity)
        {
            _db.Doctors.Update(entity);
            return Save();
        }
    }
}
