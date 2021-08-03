using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;

namespace WebDoctors.Repository
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ApplicationDbContext _db;
        public SpecializationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Specialization entity)
        {
            _db.Specializations.Add(entity);
            return Save();
        }

        public bool Delete(Specialization entity)
        {
            _db.Specializations.Remove(entity);
            return Save();
        }

        public bool Exists(int id) => _db.Specializations.Any(s => s.Id == id);

        public ICollection<Specialization> FindAll() =>
            _db.Specializations.ToList();

        public Specialization FindById(int id) =>
            _db.Specializations.SingleOrDefault(s => s.Id == id);

        public bool Save() =>
            _db.SaveChanges() > 0;

        public bool Update(Specialization entity)
        {
            _db.Specializations.Update(entity);
            return Save();
        }
    }
}
