using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class SepetDal : ISepetDal
    {
        public void AddItem(int productId, int quantity, int memberId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var existingItem = context.Sepet.FirstOrDefault(item => item.UrunId == productId && item.MusteriId==memberId);

                if (existingItem != null)
                {
                    existingItem.Adet += quantity;
                }
                else
                {
                    context.Sepet.Add(new Sepet { UrunId = productId,MusteriId=memberId,Adet=quantity});
                }
                context.SaveChanges();
            }
        }

        public double CalculateTotal(int memberId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var baskets = context.Sepet.AsNoTracking().Include(x=> x.Urun).Where(x => x.MusteriId == memberId);
                double totalPrice = 0;
                foreach (var item in baskets)
                {
                    double unitPrice = (double)item.Urun.ListeFiyat;
                    if (item.Urun.Kdv != null)
                    {
                        unitPrice = unitPrice + ((double)item.Urun.ListeFiyat * (int)item.Urun.Kdv / 100);
                    }
                    if (item.Urun.Iskonto1 != null)
                    {
                        unitPrice = unitPrice + ((double)item.Urun.ListeFiyat * (int)item.Urun.Iskonto1 / 100);
                    }
                    if (item.Urun.Iskonto2 != null)
                    {
                        unitPrice = unitPrice + ((double)item.Urun.ListeFiyat * (int)item.Urun.Iskonto2 / 100);
                    }
                    if (item.Urun.Iskonto3 != null)
                    {
                        unitPrice = unitPrice + ((double)item.Urun.ListeFiyat * (int)item.Urun.Iskonto3 / 100);
                    }
                    totalPrice += unitPrice;
                }

                return Math.Round(totalPrice,2);
            }
        }

        public void Clear(int memberId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var baskets = context.Sepet.Where(x=> x.MusteriId == memberId).ToList();
                foreach (var item in baskets)
                {
                    context.Sepet.Remove(item);
                }
                context.SaveChanges();
            }
        }

        public List<Sepet> GetItems(int memberId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                return context.Sepet.Include(x => x.Urun).Include(x => x.Musteri).Where(x=> x.MusteriId==memberId).ToList();
            }
        }

        public double KdvAmount(int productId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var item = context.Urun.AsNoTracking().FirstOrDefault(x => x.Id == productId);
                double totalPrice = (double)item.ListeFiyat;
                if (item.Kdv != null)
                {
                    totalPrice = totalPrice + ((double)item.ListeFiyat * (int)item.Kdv / 100);
                }
                if (item.Iskonto1 != null)
                {
                    totalPrice = totalPrice + ((double)item.ListeFiyat * (int)item.Iskonto1 / 100);
                }
                if (item.Iskonto2 != null)
                {
                    totalPrice = totalPrice + ((double)item.ListeFiyat * (int)item.Iskonto2 / 100);
                }
                if (item.Iskonto3 != null)
                {
                    totalPrice = totalPrice + ((double)item.ListeFiyat * (int)item.Iskonto3 / 100);
                }
                return Math.Round(totalPrice,2);
            }
        }

        public void RemoveItem(int productId,int memberId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var deletedProduct = context.Sepet.FirstOrDefault(x=> x.Urun.Id == productId && x.MusteriId==memberId);
                context.Sepet.Remove(deletedProduct);
                context.SaveChanges();
            }
        }
        public void DeleteOneProduct(int productId, int memberId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var existingItem = context.Sepet.FirstOrDefault(item => item.UrunId == productId && item.MusteriId == memberId);

                if (existingItem.Adet == 1)
                {
                    context.Sepet.Remove(existingItem);
                }
                else
                {
                    existingItem.Adet -= 1;
                }
                context.SaveChanges();
            }
        }
    }
}
