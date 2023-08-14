using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.GenericRepositoryBase
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool asNotTracking = false);
        Task<T> GetByIdAsync(int id);
    }
}
