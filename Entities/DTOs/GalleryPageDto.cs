using System.Collections.Generic;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class GalleryPageDto : IDto
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string CategoryName { get; set; }

    }

    public class GalleryPageListDto : IDto
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public List<string> CategoryName { get; set; }
        public List<string> CategoryNameList { get; set; }

    }
}