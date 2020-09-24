using Chat.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub: Hub
    {
      public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var user = Context.GetHttpContext().Request.Query["username"];
            var groups = NotificationService.GetUserGroups(user);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
           
            await base.OnDisconnectedAsync(exception);
        }
      
        public async Task MessageToAll(string message)
        {
            await Clients.All.SendAsync("MessageToAll", message);
        }

        public async Task NotificationToAll(string message)
        {
            var notification = new Notification
            {
                Time = DateTime.Now,
                Message = message
            };
            await Clients.All.SendAsync("NotificationToAll", notification);
        }

        public async Task MessageToGroup(string message, string group)
        {
            await Clients.Groups(group).SendAsync("MessageToGroup", message);
        }

        public async Task MessageToUser(string message, string user)
        {
            await Clients.Client(user).SendAsync("MessageToUser", message);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
