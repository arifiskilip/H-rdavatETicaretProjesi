using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IShoppingCartService
    {
        void AddItem(Urun urun, int quantity);
        void RemoveItem(int productId);
        void Clear();
        double CalculateTotal();
        double KdvAmount(Urun urun);
        List<Sepet> GetItems();
    }
}
