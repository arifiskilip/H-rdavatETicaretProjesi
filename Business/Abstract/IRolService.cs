using Business.GenericServiceBase;
using Entities.Concrete;
using Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRolService : IGenericService<Rol>
    {
        Task<List<Rol>> GetUserRolesAsync(int userId);
    }
}
