using DataAccess.Contexts;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UoW : IUoW
    {
     


        public void Commit()
        {
            using (HirdavatContext _context = new HirdavatContext())
            {
                _context.SaveChanges();
            }
           
        }

        public async Task CommitAsync()
        {
            using (HirdavatContext _context = new HirdavatContext())
            {
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
