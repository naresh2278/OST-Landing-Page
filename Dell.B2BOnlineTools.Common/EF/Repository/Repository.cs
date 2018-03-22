using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dell.B2BOnlineTools.Common.EF.Repository
{
    public abstract class Repository<T> : IRepository<T> where T: class
    {
        protected DbContext _dbContext;
        public Repository(DbContext dbContext) => _dbContext = dbContext;
        
        public virtual async Task<int> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<int> Delete(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Deleted;
            return await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<int> Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> expression) => 
            _dbContext.Set<T>().Where(expression).AsQueryable();        
        public virtual IQueryable<T> GetAll() => _dbContext.Set<T>().AsQueryable();             
        public virtual async Task<long> Count() => await _dbContext.Set<T>().CountAsync();
        public virtual async Task<long> Count(Expression<Func<T, bool>> expression) => await _dbContext.Set<T>().CountAsync(expression);
        public virtual IQueryable<T> Execute(string query, params object[] parameters) =>
            _dbContext.Set<T>().FromSql(query, parameters).AsQueryable();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _dbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Repository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
