using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class PasswordChangeViewModel
    {
        
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Girilen şifreler uyuşmuyor")]
        public string NewPasswordConfirm { get; set; }
    }
}