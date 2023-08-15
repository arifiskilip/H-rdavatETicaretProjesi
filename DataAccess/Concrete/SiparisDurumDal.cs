using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class SiparisDurumDal : GenericRepository<SiparisDurum>, ISiparisDurumDal
    {
        public SiparisDurumDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }
    }
}
