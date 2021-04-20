using System;
using System.Linq;
using System.Threading.Tasks;
using Entities.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Business.Hubs
{
    public class UserCountHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UCount _uCount = new UCount();

        public UserCountHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public override async Task OnConnectedAsync()
        {
            var ipAdress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _uCount.IpAdress = ipAdress;

            if (Context.User.Identity.IsAuthenticated)
            {
                UserCount.AdminUser.IpAdress = ipAdress;
                UserCount.AdminUser.ConnectionId = Context.ConnectionId;

            }
            else
            {
                if (!UserCount.IpAdresses.Any(x => x.IpAdress == _uCount.IpAdress))
                {
                    UserCount.IpAdresses.Add(_uCount);
                }
            }

            

            if (UserCount.AdminUser.ConnectionId != null)
            {
                await Clients.Client(UserCount.AdminUser.ConnectionId).SendAsync("userCount", UserCount.IpAdresses);

            }

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var ipAdress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _uCount.IpAdress = ipAdress;

            if (UserCount.IpAdresses.Any(x => x.IpAdress == _uCount.IpAdress))
            {
                var oldList = UserCount.IpAdresses.ToList();
                var currectClient = UserCount.IpAdresses.Where(x => x.IpAdress == _uCount.IpAdress).FirstOrDefault();
                oldList.Remove(currectClient);
                UserCount.IpAdresses = oldList;
            }

            if (UserCount.AdminUser.ConnectionId != null)
            {
                await Clients.Client(UserCount.AdminUser.ConnectionId).SendAsync("userCount", UserCount.IpAdresses);

            }

            if (Context.User.Identity.IsAuthenticated)
            {
                UserCount.AdminUser.IpAdress = null;
                UserCount.AdminUser.ConnectionId = null;

            }

        }
    }
}