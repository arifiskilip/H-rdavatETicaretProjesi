using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Contexts;
using Entities.Concrete;
using Entities.Enums;
using System;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMusteriService _musteriService;
        private readonly IMusteriRolService _musteriRolService;

        public AuthManager(IMusteriService musteriService, IMusteriRolService musteriRolService)
        {
            _musteriService = musteriService;
            _musteriRolService = musteriRolService;
        }

        public IDataResult<Musteri> Login(Musteri musteri)
        {
            var userToCheck = _musteriService.GetByMail(musteri.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<Musteri>("Lütfen bilgilerinizi kontrol edin.");
            }
            if (userToCheck.Email == musteri.Email && userToCheck.Sifre == musteri.Sifre)
            {
                return new SuccessDataResult<Musteri>(userToCheck, "Giriş başarılı");
            }
            return new ErrorDataResult<Musteri>("Lütfen bilgilerinizi kontrol edin.");
        }

        public async Task<IDataResult<Musteri>> RegisterAsync(Musteri musteri)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var addedCustomer =await  context.Musteri.AddAsync(musteri);
                var customerRole = new MusteriRol
                {
                    MusteriId = addedCustomer.Entity.Id,
                    RoleId = Convert.ToInt16(RolEnum.Üye) //Üye
                };
                await _musteriRolService.AddAsync(customerRole);
                await context.SaveChangesAsync();
                return new SuccessDataResult<Musteri>(musteri);
            }
          
        }

        public IResult UserExists(string email)
        {
            if (_musteriService.GetByMail(email)!=null)
            {
                return new ErrorResult("Böle bir email adresi zaten var!");
            }
            return new SuccessResult();
        }
    }
}
