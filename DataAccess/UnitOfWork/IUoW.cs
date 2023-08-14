using DataAccess.GenericRepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUoW
    {
        Task CommitAsync();
        void Commit();
    }
}
