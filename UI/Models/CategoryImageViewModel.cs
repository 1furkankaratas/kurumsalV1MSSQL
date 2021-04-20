using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class CategoryImageViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}