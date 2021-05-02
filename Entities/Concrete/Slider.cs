using Core.Entities;

namespace Entities.Concrete
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string LinkName { get; set; }
        public string Source { get; set; }
        public bool IsActive { get; set; }
    }
}