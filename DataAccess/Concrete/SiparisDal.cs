using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class SiparisDal : GenericRepository<Siparis, HirdavatContext>, ISiparisDal
    {
     
    }
}
