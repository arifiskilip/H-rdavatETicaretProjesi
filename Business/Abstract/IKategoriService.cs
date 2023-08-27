using Business.GenericServiceBase;
using Business.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IKategoriService : IGenericService<Kategori>
    {
        Task<IResult> MakeStatusPassive(int id);
    }
}
