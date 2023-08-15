using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Member.Models
{
    public class MemberChangePasswordModel
    {
        [Required(ErrorMessage = "Lütfen Eski Şifre alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen Eski geçerli bir Şifre giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen Eski geçerli bir Şifre giriniz.")]
        public string LastPassword { get; set; }
        [Required(ErrorMessage = "Lütfen Yeni Şifre alanını doldurunuz.")]
        [MinLength(1, ErrorMessage = "Lütfen Yeni geçerli bir Şifre giriniz.")]
        [MaxLength(50, ErrorMessage = "Lütfen Yeni geçerli bir Şifre giriniz.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Şifre tekrarı alanını giriniz.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmamaktadır.")]
        public string PasswordConfirm { get; set; }
    }
}
