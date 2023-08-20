using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using Entities.Enums;

namespace DataAccess.Concrete
{
    public class MusteriDal : GenericRepository<Musteri>, IMusteriDal
    {
        public MusteriDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }

        public List<UserDto> GetAllCustomers()
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var result = from ur in context.MusteriRol
                             join
                             u in context.Musteri on ur.MusteriId equals u.Id
                             where ur.RoleId == (int)RolEnum.Üye
                             select new UserDto
                             {
                                 Id = u.Id,
                                 Ad = u.Ad,
                                 Soyad = u.Soyad,
                                 Adres = u.Adres,
                                 FirmaAd = u.FirmaAd,
                                 Resim = u.Resim,
                                 Telefon = u.Telefon,
                                 IlceId = u.IlceId,
                                 IlId = u.IlId,
                                 CreateDate = u.CreateDate
                             };
                return result.ToList();
            }
        }
    }
}
