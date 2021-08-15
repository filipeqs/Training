using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDoctors.Contracts
{
    public interface IDocumetUpload<T>
    {
        bool SaveFile(T file);

        string GetFilePath(T file);
    }
}
