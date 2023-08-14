using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class MarkaDal : GenericRepository<Marka, HirdavatContext>, IMarkaDal
    {
       
    }
}
