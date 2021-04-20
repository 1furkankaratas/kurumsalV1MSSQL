using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class SocialMediaViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Link { get; set; }

    }
}