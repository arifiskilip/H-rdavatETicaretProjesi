using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Member.Models
{
    public class MemberUpdateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="İsim alanı boş geçilemez.")]
        [MinLength(1,ErrorMessage ="Lütfen geçerli bir isim giriniz.")]
        [MaxLength(20,ErrorMessage ="Lütfen geçerli bir isim giriniz.")]
        public string? Ad { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş geçilemez.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir soyisim giriniz.")]
        [MaxLength(20, ErrorMessage = "Lütfen geçerli bir soyisim giriniz.")]
        public string? Soyad { get; set; }
        public string? FirmaAd { get; set; }
        public int? IlId { get; set; }
        public IFormFile? Resim { get; set; }
        public string? MevcutResim { get; set; }
        public int? IlceId { get; set; }
        [Required(ErrorMessage = "Telefon alanı boş geçilemez.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir telefon giriniz.")]
        [MaxLength(20, ErrorMessage = "Lütfen geçerli bir telefon giriniz.")]
        [Phone(ErrorMessage = "Lütfen geçerli bir telefon giriniz.")]
        public string? Telefon { get; set; }
        [Required(ErrorMessage = "Adres alanı boş geçilemez.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir adres giriniz.")]
        [MaxLength(100, ErrorMessage = "Lütfen geçerli bir adres giriniz.")]
        public string? Adres { get; set; }
    }
}
