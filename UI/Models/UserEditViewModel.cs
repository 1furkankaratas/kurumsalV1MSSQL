using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class UserEditViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}