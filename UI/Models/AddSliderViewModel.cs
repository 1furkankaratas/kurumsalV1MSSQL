using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class AddSliderViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string LinkName { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
