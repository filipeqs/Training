using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Data;

namespace WebDoctors.Contracts
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
    }
}
