using Business.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;
namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<Musteri>> RegisterAsync(Musteri musteri);
        IDataResult<Musteri> Login(Musteri musteri);
        IResult UserExists(string email);
    }
}
