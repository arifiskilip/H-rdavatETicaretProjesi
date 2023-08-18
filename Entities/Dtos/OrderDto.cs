using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime? Tarih { get; set; }
        public string MusteriAdi { get; set; }
        public int? MusteriId { get; set; }
        public string DurumAdi { get; set; }
        public string SirketAdi { get; set; }
        public string Telefon { get; set; }
        public int? DurumId { get; set; }
        public double? ToplamFiyat { get; set; }
        public double? KDVTutar { get; set; }
        public double? Indirim { get; set; }
        public double? FaturaTutari { get; set; }
    }
}
