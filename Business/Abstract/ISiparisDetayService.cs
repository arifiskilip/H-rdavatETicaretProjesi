using Business.GenericServiceBase;
using Business.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISiparisDetayService : IGenericService<SiparisDetay>
    {
        IDataResult<List<OrderDetailDto>> GetAllByOrderId(int orderId);

    }
}
