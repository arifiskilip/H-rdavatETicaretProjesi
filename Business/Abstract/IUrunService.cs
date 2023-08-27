using Business.GenericServiceBase;
using Business.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUrunService : IGenericService<Urun>
    {
        Task<IDataResult<List<Urun>>> ProductsWithCategoryIdGet(int categoryId);
        Task<IDataResult<List<Urun>>> ProductsWithBrandIdGet(int brandId);
        IDataResult<UrunDto> GetProductDto(int id);
        IDataResult<List<UrunDto>> GetProductListDto();
        Task<IResult> MakeStatusPassive(int id);
    }
}
