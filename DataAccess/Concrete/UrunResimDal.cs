using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class UrunResimDal : GenericRepository<UrunResim>, IUrunResimDal
    {
        public UrunResimDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }
    }
}
