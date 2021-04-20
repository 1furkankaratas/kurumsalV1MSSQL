using Core.Entities;

namespace Entities.Concrete
{
    public class GalleryCategory : IEntity
    {
        public int Id { get; set; }
        public int GalleryImageId { get; set; }
        public int CategoryImageId { get; set; }
    }
}