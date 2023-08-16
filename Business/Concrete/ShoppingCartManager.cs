using Business.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ShoppingCartManager : IShoppingCartService
    {
        private readonly List<Sepet> items = new List<Sepet>();
    

        public void AddItem(Urun urun, int quantity)
        {
            var existingItem = items.FirstOrDefault(item => item.Urun.Id == urun.Id);

            if (existingItem != null)
            {
                existingItem.Adet += quantity;
            }
            else
            {
                items.Add(new Sepet { Urun = urun, Adet = quantity });
            }
        }

        public List<Sepet> GetItems()
        {
            return items;
        }

        public double CalculateTotal()
        {
            var carts = items.ToList();
            double totalPrice = 0;
            foreach (var item in carts)
            {
                if (item.Urun.Kdv != null)
                {
                    totalPrice = (totalPrice * (int)item.Urun.Kdv / 100);
                }
                if (item.Urun.Iskonto1 != null)
                {
                    totalPrice = (totalPrice * (int)item.Urun.Iskonto1 / 100);
                }
                if (item.Urun.Iskonto2 != null)
                {
                    totalPrice =  (totalPrice * (int)item.Urun.Iskonto2 / 100);
                }
                if (item.Urun.Iskonto3 != null)
                {
                    totalPrice = (totalPrice * (int)item.Urun.Iskonto3 / 100);
                }
                totalPrice += (double)item.Urun.ListeFiyat * (int)item.Adet; 
            }

            return totalPrice;
        }

        public void Clear()
        {
            items.Clear();
        }

        public double KdvAmount(Urun urun)
        {
            var item = items.FirstOrDefault(item => item.Urun.Id == urun.Id);
            double totalPrice = 0;
            if (item.Urun.Kdv != null)
            {
                totalPrice = (totalPrice * (int)item.Urun.Kdv / 100);
            }
            if (item.Urun.Iskonto1 != null)
            {
                totalPrice = (totalPrice * (int)item.Urun.Iskonto1 / 100);
            }
            if (item.Urun.Iskonto2 != null)
            {
                totalPrice = (totalPrice * (int)item.Urun.Iskonto2 / 100);
            }
            if (item.Urun.Iskonto3 != null)
            {
                totalPrice = (totalPrice * (int)item.Urun.Iskonto3 / 100);
            }
            totalPrice += (double)item.Urun.ListeFiyat * (int)item.Adet;
            return totalPrice;
        }

        public void RemoveItem(int productId)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Urun.Id == productId);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }
    }
    //public IEnumerable<ShoppingCartItem> Items => items;
}
