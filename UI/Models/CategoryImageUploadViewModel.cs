using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class CategoryImageUploadViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}