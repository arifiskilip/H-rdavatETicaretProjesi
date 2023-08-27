using Business.GenericServiceBase;
using Business.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMusteriService : IGenericService<Musteri>
    {
        Musteri GetByPhone(string phone);
        Task<IResult> MakeStatusPassive(int id);
        Task<IResult> MakeStatusActive(int id);
    }
}
