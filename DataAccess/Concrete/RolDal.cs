using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class RolDal : GenericRepository<Rol>, IRolDal
    {
        public RolDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }
    }
}
