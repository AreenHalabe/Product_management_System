using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class Admin
    {
        public Admin()
        {
            NotificationIds = new HashSet<NotificationId>();
        }

        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<NotificationId> NotificationIds { get; set; }
    }
}
