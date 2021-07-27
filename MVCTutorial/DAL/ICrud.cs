using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTutorial.DAL
{
    interface ICrud<T>
    {
        List<T> FetchAll();
        T Fetch(int id);
        string Create(T value);
        string Update(T value);
        string Delete(int id);
    }
}
