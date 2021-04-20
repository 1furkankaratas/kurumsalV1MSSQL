using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class CategoryImageSelectBoxViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsSelected { get; set; }
    }
}