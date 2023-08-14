using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUrunDal : IGenericRepository<Urun>
    {
        UrunDto GetProductDto(int id);
        List<UrunDto> GetProductListDto();
    }
}
