using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class MusteriRolDal : GenericRepository<MusteriRol>, IMusteriRolDal
    {
        public MusteriRolDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }
    }
}
