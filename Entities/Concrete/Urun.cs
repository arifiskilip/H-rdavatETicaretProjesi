using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Urun
    {
        public int Id { get; set; }
        public string? Kod { get; set; }
        public string? Ad { get; set; }
        public int? KategoriId { get; set; }
        public int? MarkaId { get; set; }
        public bool? StokDurum { get; set; }
        public double? ListeFiyat { get; set; }
        public int? Kdv { get; set; }
        public int? Iskonto1 { get; set; }
        public int? Iskonto2 { get; set; }
        public int? Iskonto3 { get; set; }
        public int? KutuAdet { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public List<Sepet> Sepets { get; set; }
    }
}
