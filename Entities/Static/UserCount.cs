using System.Collections.Generic;

namespace Entities.Static
{
    public static class UserCount
    {
        public static List<UCount> IpAdresses { get; set; } = new List<UCount>();
        public static ACount AdminUser { get; set; } = new ACount();
    }
}