using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Lütfen İsim alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir isim giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen geçerli bir isim giriniz.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen Soyisim alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir soyisim giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen geçerli bir soyisim giriniz.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Lütfen telefon alanını doldurunuz.")]
        [Phone(ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        [MinLength(5, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.('05555555555')")]
        [MaxLength(50, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.('05555555555')")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen geçerli bir Şifre giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen geçerli bir Şifre giriniz.")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Şifre tekrarı alanını giriniz.")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmamaktadır.")]
        public string PasswordConfirm { get; set; }
    }
}
