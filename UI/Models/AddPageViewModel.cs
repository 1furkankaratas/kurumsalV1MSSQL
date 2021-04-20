using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UI.Models
{
    public class AddPageViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Type { get; set; }
    }
}