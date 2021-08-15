using AutoMapper;
using WebDoctors.Data;
using WebDoctors.Models;

namespace WebDoctors.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Person, PersonVM>().ReverseMap();
            CreateMap<Doctor, DoctorVM>().ReverseMap();
            CreateMap<Specialization, SpecializationVM>().ReverseMap();
            CreateMap<Schedule, ScheduleVM>().ReverseMap();
            CreateMap<ScheduleTime, ScheduleTimeVM>().ReverseMap();
            CreateMap<Appointment, AppointmentVM>().ReverseMap();
        }
    }
}
