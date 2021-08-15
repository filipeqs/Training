using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Data;

namespace WebDoctors.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Doctor> Doctors { get; }
        IRepository<Specialization> Specializations { get; }
        IRepository<Schedule> Schedules { get; }
        IRepository<ScheduleTime> ScheduleTimes { get; }
        IRepository<Appointment> Appointments { get; }
        Task Save();
    }
}
