using Core.Entities;

namespace Entities.Concrete
{
    public class Setting : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Meta { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CompanyName { get; set; }
        public string WorkTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Maps { get; set; }
        public string Favicon { get; set; }
        public string WhatsAppPhone { get; set; }
        public string WhatsAppText { get; set; }
    }
}