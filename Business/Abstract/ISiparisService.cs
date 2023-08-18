using Business.GenericServiceBase;
using Business.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISiparisService : IGenericService<Siparis>
    {
        IDataResult<List<OrderDto>> GetAllByOrderBy();
        IDataResult<List<OrderDto>> GetAllByCustomerId(int customerId);
        IDataResult<List<OrderDto>> GetAllByCompleted(int customerId);
        IDataResult<List<OrderDto>> GetAllByCanceled(int customerId);
        IDataResult<List<OrderDto>> GetAllByCompleted();
        IDataResult<List<OrderDto>> GetAllByCanceled();
    }
}
