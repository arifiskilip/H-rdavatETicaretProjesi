using DataAccess.Contexts;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UoW : IUoW
    {

        private HirdavatContext _context;

        public UoW(HirdavatContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
