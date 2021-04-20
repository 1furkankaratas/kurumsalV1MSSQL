using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UI.Models
{
    public class UpdatePageViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Type { get; set; }
    }
}