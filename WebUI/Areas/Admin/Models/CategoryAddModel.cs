using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.Models
{
    public class CategoryAddModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Lütfen marka adını giriniz.")]
        [MinLength(1,ErrorMessage ="Lütfen geçerli bir marka adı giriniz.")]
        [MaxLength(50,ErrorMessage ="Lütfen geçerli bir marka adı giriniz.")]
        public string? Ad { get; set; }
    }
}
