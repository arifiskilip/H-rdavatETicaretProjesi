using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Helpers
{
    public static class CalculateHalpers
    {
        public static int CalculateKdv(int productId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var product = context.Urun.FirstOrDefault(x=> x.Id== productId);
                if (product.Kdv != null)
                {
                    return (int)product.Kdv;
                }
                return 0;
            }
        }
        public static int CalculateIskonto(int productId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var product = context.Urun.FirstOrDefault(x => x.Id == productId);
                int result = 0;
                if (product.Iskonto1 != null)
                {
                    result += (int)product.Iskonto1;
                }
                if (product.Iskonto2 != null)
                {
                    result += (int)product.Iskonto2;
                }
                if (product.Iskonto3 != null)
                {
                    result += (int)product.Iskonto3;
                }
                return result;
            }
        }
        public static double KdvAmount(int productId, int quantitiy)
        {
            using (HirdavatContext context = new HirdavatContext())
            {

                var item = context.Urun.AsNoTracking().FirstOrDefault(item => item.Id == productId);
                double totalPrice = (double)item.ListeFiyat;
                if (item.Kdv != null)
                {
                    totalPrice = totalPrice + ((double)item.ListeFiyat * (int)item.Kdv / 100);
                }
                if (item.Iskonto1 != null)
                {
                    totalPrice = totalPrice - ((double)item.ListeFiyat * (int)item.Iskonto1 / 100);
                }
                if (item.Iskonto2 != null)
                {
                    totalPrice = totalPrice - ((double)item.ListeFiyat * (int)item.Iskonto2 / 100);
                }
                if (item.Iskonto3 != null)
                {
                    totalPrice = totalPrice - ((double)item.ListeFiyat * (int)item.Iskonto3 / 100);
                }
                return Math.Round(totalPrice*quantitiy, 2);
            }
        }
    }
}
