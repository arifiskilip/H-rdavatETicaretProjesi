using Business.Abstract;
using DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Helpers
{
    public static class UserHelper
    {
       public static string GetCity(int cityId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                string city = context.Il.Find(cityId).Ad;
                if (city != null)
                {
                    return city;
                }
                return "Belirtilmemiş";
            }
        }
        public static string GetDistrict(int districtId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                string district = context.Ilce.Find(districtId).Ad;
                if (district != null)
                {
                    return district;
                }
                return "Belirtilmemiş";
            }
        }
    }
}
