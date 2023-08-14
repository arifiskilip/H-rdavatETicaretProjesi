using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.GenericRepositoryBase
{
    public class GenericRepository<T,TContext> : IGenericRepository<T> where T : class, new()
        where TContext:DbContext,new()
    {
     
        public async Task AddAsync(T entity)
        {
            using (TContext _context = new TContext())
            {
                await _context.Set<T>().AddAsync(entity);
            }  
        }

        public void Delete(T entity)
        {
            using (TContext _context = new TContext())
            {
                _context.Set<T>().Remove(entity);
            }   
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool asNotTracking = false)
        {
            using (TContext _context = new TContext())
            {
                return !asNotTracking ?
               (filter == null ? await _context.Set<T>().ToListAsync()
               : await _context.Set<T>().Where(filter).ToListAsync())
               : (filter == null ? await _context.Set<T>().AsNoTracking().ToListAsync()
               : await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync());
            }
           
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (TContext _context = new TContext())
            {
                return await _context.Set<T>().FindAsync(id);
            }
           
        }

        public void Update(T entity)
        {
            using (TContext _context = new TContext())
            {
                _context.Set<T>().Update(entity);
            }
            
        }
    }
}
