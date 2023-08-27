using Business.GenericServiceBase;
using Business.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMarkaService : IGenericService<Marka>
    {
        Task<IResult> MakeStatusPassive(int id);
    }
}
