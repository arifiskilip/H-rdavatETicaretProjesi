using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class IlDal : GenericRepository<Il>, IIlDal
    {
        public IlDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }
    }
}
