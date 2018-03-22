using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dell.B2BOnlineTools.Common.EF.Repository
{
    public interface IRepository<T> : IDisposable where T: class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task<int> Add(T entity);
        Task<int> Delete(T entity);
        Task<int> Update(T entity);
        IQueryable<T> Execute(string query, params object[] parameters);

        Task<long> Count();
        Task<long> Count(Expression<Func<T, bool>> expression);
    }
}
