using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebDoctors.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> FindAll(
                Expression<Func<T, bool>> expression = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
            );

        Task<T> Find(
                Expression<Func<T, bool>> expression = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
            );

        Task<bool> Exists(Expression<Func<T, bool>> expression);

        Task Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
