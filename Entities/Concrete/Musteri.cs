using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Musteri
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? FirmaAd { get; set; }
        public string? Email { get; set; }
        public string? Resim { get; set; }
        public int? IlId { get; set; }
        public int? IlceId { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public string? Sifre { get; set; }
        public DateTime? CreateDate { get; set; }=DateTime.Now;

        public List<Sepet> Sepets { get; set; }
        public List<Siparis> Siparis { get; set; }

    }
}
