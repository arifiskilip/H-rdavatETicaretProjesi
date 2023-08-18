using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ISiparisDetayDal : IGenericRepository<SiparisDetay>
    {
        List<OrderDetailDto> GetAllByOrderId(int orderId);
    }
}
