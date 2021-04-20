using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UI.Models
{
    public class AddGalleryImageViewModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public List<int> CategoriesId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}