using Core.Entities;

namespace Entities.Concrete
{
    public class SocialMedia : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
    }
}