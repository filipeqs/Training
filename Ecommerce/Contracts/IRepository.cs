using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Ecommerce.Contracts
{
    public interface IRepository<T>
    {
        IList<T> FindAll(Expression<Func<T, bool>> expression);
    }
}