using Core.Entities;

namespace Entities.Concrete
{
    public class Page : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}