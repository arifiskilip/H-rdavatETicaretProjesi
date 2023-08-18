using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.GenericRepositoryBase;
using DataAccess.Helpers;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete
{
    public class SiparisDetayDal : GenericRepository<SiparisDetay>, ISiparisDetayDal
    {
        public SiparisDetayDal(HirdavatContext hirdavatContext) : base(hirdavatContext)
        {
        }

        public List<OrderDetailDto> GetAllByOrderId(int orderId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                //var result = context.SiparisDetay.Include(x => x.Urun).Include(x => x.Urun.Marka).Include(x => x.Siparis)
                //.Include(x => x.Siparis.Musteri).AsNoTracking().Where(x=> x.SiparisId == orderId).ToList();
                var siparisDtoList = from siparisDetay in context.SiparisDetay
                                     join urun in context.Urun on siparisDetay.UrunId equals urun.Id
                                     join siparis in context.Siparis on siparisDetay.SiparisId equals siparis.Id
                                     join marka in context.Marka on urun.MarkaId equals marka.Id
                                     where siparisDetay.SiparisId == orderId
                                     select new OrderDetailDto
                                     {
                                         SiparisId = siparisDetay.SiparisId,
                                         UrunId = siparisDetay.UrunId,
                                         TalepAdet = siparisDetay.TalepAdet,
                                         TeslimAdet = siparisDetay.TeslimAdet,
                                         UrunFiyat = (double)urun.ListeFiyat,
                                         UrunMarka = marka.Ad,
                                         CreateDate = siparisDetay.CreateDate,
                                         UrunIsım = urun.Ad,
                                         Id= siparisDetay.Id,
                                         ToplamIskonto = CalculateHalpers.CalculateIskonto(urun.Id),
                                         ToplamKdv = CalculateHalpers.CalculateKdv(urun.Id),
                                         UrunKod = urun.Kod,
                                         ToplamTutar = (double)siparisDetay.Fiyat,
                                         DurumId = (int)siparis.DurumId
                                     };
                var a = siparisDtoList.ToList();
                return a;
            }
        }
       
    }
}
