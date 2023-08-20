using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Lütfen telefon numaranızı giriniz.")]
        [Phone(ErrorMessage ="Lütfen geçerli bir numara giriniz.")]
        [MinLength(11,ErrorMessage = "Lütfen geçerli bir numara giriniz.")]
        [MaxLength(11,ErrorMessage = "Lütfen geçerli bir numara giriniz.")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Lütfen şifre alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir Şifre giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen geçerli bir Şifre giriniz.")]
        public string Password { get; set; }
    }
}
