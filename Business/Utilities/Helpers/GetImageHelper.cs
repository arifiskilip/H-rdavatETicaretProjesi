using DataAccess.Contexts;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Utilities.Helpers
{
    public static class GetImageHelper
    {
        public static List<UrunResim> GetProductImagesById(int productId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var productImage = context.UrunResim.Where(x => x.UrunId == productId).ToList();
                return productImage;
            }
        }
    }
}
