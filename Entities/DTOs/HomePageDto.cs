using System.Collections.Generic;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class HomePageDto:IDto
    {
        public List<Slider> Sliders { get; set; }
        public List<Page> Pages { get; set; }
    }
}