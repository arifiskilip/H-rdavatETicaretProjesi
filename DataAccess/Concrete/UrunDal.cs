using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Concrete
{
    public class UrunDal : GenericRepository<Urun, HirdavatContext>, IUrunDal
    {
        public  UrunDto GetProductDto(int id)
        {
            using (HirdavatContext context = new HirdavatContext())
            {

                try
                {
                    var result = from p in context.Urun
                                 join c in context.Kategori
                                 on p.KategoriId equals c.Id
                                 join b in context.Marka
                                 on p.MarkaId equals b.Id
                                 where p.Id == id
                                 select new UrunDto
                                 {
                                     Id = p.Id,
                                     Ad = p.Ad,
                                     CreateDate = p.CreateDate,
                                     Iskonto1 = p.Iskonto1,
                                     Iskonto2 = p.Iskonto2,
                                     Iskonto3 = p.Iskonto3,
                                     KategoriAdi = c.Ad,
                                     KategoriId = p.KategoriId,
                                     Kod = p.Kod,
                                     KutuAdet = p.KutuAdet,
                                     ListeFiyat = p.ListeFiyat,
                                     MaraAdi = b.Ad,
                                     MarkaId = p.MarkaId,
                                     StokDurum = p.StokDurum
                                 };
                    return result.FirstOrDefault();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        public List<UrunDto> GetProductListDto()
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var result = from p in context.Urun
                             join c in context.Kategori
                             on p.KategoriId equals c.Id
                             join b in context.Marka
                             on p.MarkaId equals b.Id
                             select new UrunDto
                             {
                                 Id = p.Id,
                                 Ad = p.Ad,
                                 CreateDate = p.CreateDate,
                                 Iskonto1 = p.Iskonto1,
                                 Iskonto2 = p.Iskonto2,
                                 Iskonto3 = p.Iskonto3,
                                 KategoriAdi = c.Ad,
                                 KategoriId = p.KategoriId,
                                 Kod = p.Kod,
                                 KutuAdet = p.KutuAdet,
                                 ListeFiyat = p.ListeFiyat,
                                 MaraAdi = b.Ad,
                                 MarkaId = p.MarkaId,
                                 StokDurum = p.StokDurum
                             };
                return result.ToList();
            }
        }
    }
}
