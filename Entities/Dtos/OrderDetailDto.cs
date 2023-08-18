using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int? SiparisId { get; set; }
        public int? UrunId { get; set; }
        public int? TalepAdet { get; set; }
        public int? TeslimAdet { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UrunKod { get; set; }
        public string UrunIsım { get; set; }
        public string UrunMarka { get; set; }
        public double UrunFiyat { get; set; }
        public int ToplamIskonto { get; set; }
        public int ToplamKdv { get; set; }
        public int DurumId { get; set; }
        public double ToplamTutar { get; set; }
    }
}
