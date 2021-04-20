using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UI.Models
{
    public class SettingViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Meta { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile Logo { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string WorkTime { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Maps { get; set; }
        [Required]
        public string WhatsAppPhone { get; set; }
        [Required]
        public string WhatsAppText { get; set; }
        public IFormFile Favicon { get; set; }
    }
}