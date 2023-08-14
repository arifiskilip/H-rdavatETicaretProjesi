using Business.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.GenericServiceBase
{
    public interface IGenericService<T> where T : class, new()
    {
        Task<IResult> AddAsync(T entity);
        Task<IResult> UpdateAsync(T entity);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<List<T>>> GetAllAsync();
        Task<IDataResult<T>> GetByIdAsync(int id);
    }
}
