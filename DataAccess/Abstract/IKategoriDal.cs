using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IKategoriDal : IGenericRepository<Kategori>
    {
        List<KategoriListDto> KategoriListDtos();
    }
}
