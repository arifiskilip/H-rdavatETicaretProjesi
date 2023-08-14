using System;

namespace Entities.Concrete
{
    public class Siparis
    {
        public int Id { get; set; }
        public DateTime? Tarih { get; set; }
        public int? MusteriId { get; set; }
        public int? DurumId { get; set; }
        public double? ToplamFiyat { get; set; }
        public double? KDVTutar { get; set; }
        public double? Indirim { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
