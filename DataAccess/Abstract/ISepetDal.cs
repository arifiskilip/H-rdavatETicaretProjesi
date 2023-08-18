using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ISepetDal
    {
        void AddItem(int productId, int quantity, int memberId);
        void RemoveItem(int productId, int memberId);
        void Clear(int memberId);
        double CalculateTotal(int memberId);
        public double KdvAmount(int productId);
        void DeleteOneProduct(int productId, int memberId);
        int CalculateKdv(int memberId);
        List<Sepet> GetItems(int memberId);
    }
}
