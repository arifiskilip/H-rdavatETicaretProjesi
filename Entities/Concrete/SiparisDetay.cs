using System;

namespace Entities.Concrete
{
    public class SiparisDetay
    {
        public int Id { get; set; }
        public int? SiparisId { get; set; }
        public int? UrunId { get; set; }
        public int? TalepAdet { get; set; }
        public int? TeslimAdet { get; set; }
        public double? Fiyat { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
