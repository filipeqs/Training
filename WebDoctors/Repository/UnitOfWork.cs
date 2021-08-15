using System;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;

namespace WebDoctors.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;
        private IRepository<Doctor> _doctors;
        private IRepository<Specialization> _specializations;
        private IRepository<Schedule> _schedules;
        private IRepository<ScheduleTime> _scheduleTimes;
        private IRepository<Appointment> _appointments;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Doctor> Doctors => _doctors ??= new Repository<Doctor>(_context);
        public IRepository<Specialization> Specializations => _specializations ??= new Repository<Specialization>(_context);
        public IRepository<Schedule> Schedules => _schedules ??= new Repository<Schedule>(_context);
        public IRepository<ScheduleTime> ScheduleTimes => _scheduleTimes ??= new Repository<ScheduleTime>(_context);
        public IRepository<Appointment> Appointments => _appointments ??= new Repository<Appointment>(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
                _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
