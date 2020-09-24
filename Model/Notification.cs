using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Model
{
    public class Notification
    {
        //public Guid NotificationID { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        //public bool IsSend { get; set; }
        public string Group { get; set; }
    }
}
