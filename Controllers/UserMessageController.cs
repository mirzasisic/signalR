using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Hubs;
using Chat.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _signalChatHubContext;
        private List<UserGroup> _userGroups = new List<UserGroup>();
        private List<Notification> _notifications;
        public UserMessageController(IHubContext<ChatHub> signalChatHubContext)
        {
            _signalChatHubContext = signalChatHubContext;
            //_userGroups = new List<UserGroup>();
            //_notifications = new List<Notification>();
        }

        [HttpPost]
        [Route("/SendToAll")]
        public async Task SendToAll(string message)
        {
            await _signalChatHubContext.Clients.All.SendAsync("MessageToAll", message);
        }

        [HttpPost]
        [Route("/AddRealUserToGroup")]
        public void AddRealUserToGroup(string user, string group)
        {
            NotificationService.AddUserToGroup(user, group);
        }

        [HttpPost]
        [Route("/SendToGroup")]
        public async Task SendToGroup(string group, string message)
        {
            //_notificationService.AddNotification(new Notification
            //{
            //    Group = group,
            //    Message = message,
            //    Time = DateTime.Now
            //});
            message += " - " + group;
            await _signalChatHubContext.Clients.Group(group).SendAsync("MessageToGroup", message);
        }

        [HttpPost]
        [Route("/SendToUser")]
        public async Task SendToUser(string user, string message)
        {
            await _signalChatHubContext.Clients.Client(user).SendAsync("MessageToUser", message);
        }


        [HttpPost]
        [Route("/AddUserToGroup")]
        public async Task AddUserToGroup(string user, string group)
        {
            await _signalChatHubContext.Groups.AddToGroupAsync(user, group);
            await _signalChatHubContext.Clients.Group(group).SendAsync("MessageToGroup", user + " has been added to the " + group);
        }

        [HttpPost]
        [Route("/RemoveUserFromGroup")]
        public async Task RemoveUserFromGroup(string user, string group)
        {
            await _signalChatHubContext.Groups.RemoveFromGroupAsync(user, group);
            await _signalChatHubContext.Clients.Client(user).SendAsync("You have been removed from group");
            await _signalChatHubContext.Clients.Group(group).SendAsync("MessageToGroup", user + " has been removed from the " + group);
        }

    }
}
