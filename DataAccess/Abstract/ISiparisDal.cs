using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ISiparisDal : IGenericRepository<Siparis>
    {
        List<OrderDto> GetAllByCustomerId(int customerId);
        List<OrderDto> GetAllByOrderBy();

    }
}
