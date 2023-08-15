using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class SiparisDetayDal : GenericRepository<SiparisDetay>, ISiparisDetayDal
    {
        public SiparisDetayDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }
    }
}
