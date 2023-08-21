using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.Models
{
    public class ProductAddModel
    {

        public int Id { get; set; }
        public string? Kod { get; set; }
        [Required(ErrorMessage ="Ürün adı boş geçilemez.")]
        public string? Ad { get; set; }
        public bool? StokDurum { get; set; }
        [Required(ErrorMessage ="Fiyat alanı boş geçielemez.")]
        public double? ListeFiyat { get; set; }
        [Required(ErrorMessage ="Kategori alanı boş geçilemez.")]
        public int? KategoriId { get; set; }
        [Required(ErrorMessage = "Marka alanı boş geçilemez.")]
        public int? MarkaId { get; set; }
        public int? Iskonto1 { get; set; }
        public int? Iskonto2 { get; set; }
        public int? Iskonto3 { get; set; }
        [Required(ErrorMessage = "Kutu Adet alanı boş geçielemez.")]
        [Range(1,1000,ErrorMessage = "Kutu adet alanı 1'den küçük olamaz.")]
        public int? KutuAdet { get; set; }
        [Required(ErrorMessage = "Stok alanı boş geçielemez.")]
        [Range(1,1000,ErrorMessage ="Stok alanı 1'den küçük olamaz.")]
        public int? Stok { get; set; }

        public SelectList Kategoriler { get; set; }
        public SelectList Markalar { get; set; }

    }
}
