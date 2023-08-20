using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? FirmaAd { get; set; }
        public string? Resim { get; set; }
        public string? Il { get; set; }
        public int? IlId { get; set; }
        public int? IlceId { get; set; }
        public string? Ilce { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public DateTime? CreateDate { get; set; } 
    }
}
