using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete
{
    public class KategoriDal : GenericRepository<Kategori, HirdavatContext>, IKategoriDal
    {
        public List<KategoriListDto> KategoriListDtos()
        {
            throw new System.NotImplementedException();
        }
    }
}
