using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UrunDto
    {
        public int Id { get; set; }
        public string? Kod { get; set; }
        public string? Ad { get; set; }
        public string KategoriAdi { get; set; }
        public int? KategoriId { get; set; }
        public string MaraAdi { get; set; }
        public int? MarkaId { get; set; }
        public bool? StokDurum { get; set; }
        public double? ListeFiyat { get; set; }
        public int? Iskonto1 { get; set; }
        public int? Iskonto2 { get; set; }
        public int? Iskonto3 { get; set; }
        public int? KutuAdet { get; set; }
        public int? Stok { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
