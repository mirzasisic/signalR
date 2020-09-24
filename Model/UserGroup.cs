using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Model
{
    public class UserGroup
    {
        public Guid Id { get; set;  }
        public string User { get; set; }
        public string Group { get; set; }
    }
}
