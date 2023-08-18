using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using Entities.Concrete;
using Entities.Dtos;
using System.Linq;
using System.Collections.Generic;

namespace DataAccess.Concrete
{
    public class SiparisDal : GenericRepository<Siparis>, ISiparisDal
    {
        public SiparisDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
           
        }

        public List<OrderDto> GetAllByCustomerId(int customerId)
        {
            using(HirdavatContext context = new HirdavatContext())
            {
                var result = from s in context.Siparis
                             join
                             m in context.Musteri on
                             s.MusteriId equals m.Id
                             join d in context.SiparisDurum on
                             s.DurumId equals d.Id
                             where s.MusteriId == customerId
                             orderby s.CreateDate descending
                             select new OrderDto
                             {
                                 Id = s.Id,
                                 DurumAdi = d.Ad,
                                 DurumId = d.Id,
                                 FaturaTutari = s.FaturaTutari,
                                 Indirim = s.Indirim,
                                 KDVTutar = s.KDVTutar,
                                 MusteriAdi = m.Ad + " " + m.Soyad,
                                 MusteriId = s.MusteriId,
                                 Tarih = s.Tarih,
                                 ToplamFiyat = s.ToplamFiyat,
                                 SirketAdi = m.FirmaAd,
                                 Telefon = m.Telefon,
                                 
                             };

                return result.ToList();             
            }
        }

        public List<OrderDto> GetAllByOrderBy()
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var result = from s in context.Siparis
                             join
                             m in context.Musteri on
                             s.MusteriId equals m.Id
                             join d in context.SiparisDurum on
                             s.DurumId equals d.Id
                             orderby s.Tarih
                             select new OrderDto
                             {
                                 Id = s.Id,
                                 DurumAdi = d.Ad,
                                 DurumId = d.Id,
                                 FaturaTutari = s.FaturaTutari,
                                 Indirim = s.Indirim,
                                 KDVTutar = s.KDVTutar,
                                 MusteriAdi = m.Ad + " " + m.Soyad,
                                 MusteriId = s.MusteriId,
                                 Tarih = s.Tarih,
                                 ToplamFiyat = s.ToplamFiyat,
                                 SirketAdi = m.FirmaAd,
                                 Telefon = m.Telefon
                             };

                return result.OrderByDescending(x => x.Tarih).ToList();
            }
        }
    }
}
