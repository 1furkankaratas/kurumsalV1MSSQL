using Core.Entities;

namespace Entities.Concrete
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
    }
}