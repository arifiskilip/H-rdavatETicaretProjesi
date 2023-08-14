using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Lütfen Email alanını doldurunuz.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir Email adresi giriniz.")]
        [MinLength(5,ErrorMessage = "Lütfen geçerli bir Email adresi giriniz.")]
        [MaxLength(50,ErrorMessage = "Lütfen geçerli bir Email adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir Şifre giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen geçerli bir Şifre giriniz.")]
        public string Password { get; set; }
    }
}
