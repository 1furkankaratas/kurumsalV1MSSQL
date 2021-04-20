using Core.Entities;

namespace Entities.DTOs
{
    public class ContactPageDto : IDto
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Maps { get; set; }
        public string WorkTime { get; set; }
    }
}