using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UI.Models
{
    public class UpdateGalleryPageModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        [Required]
        public List<int> CategoriesId { get; set; }
    }
}