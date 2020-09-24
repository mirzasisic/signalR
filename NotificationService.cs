using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat
{
    public static class NotificationService
    {
        private static List<UserGroup> _userGroups = new List<UserGroup>();
        private static List<Notification> _notifications = new List<Notification>();
        
        public static void AddUserToGroup(string user, string group)
        {
            _userGroups.Add(new UserGroup
            {
                Group = group,
                User = user,
                Id = Guid.NewGuid()
            });
        }

        public static void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public static List<UserGroup> GetUserGroups(string user)
        {
            return _userGroups.Where(x => x.User == user).ToList();
        }

        public static List<Notification> GetUserNotification(string user)
        {
            return _notifications.Where(x => x.Group == user).ToList();
        }
    }
}
