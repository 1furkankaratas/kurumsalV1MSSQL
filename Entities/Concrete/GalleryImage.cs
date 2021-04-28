using Core.Entities;

namespace Entities.Concrete
{
    public class GalleryImage : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public bool IsActive { get; set; }
    }
}