using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class Supplier
    {
        public Supplier()
        {
            NotificationIds = new HashSet<NotificationId>();
            Transactions = new HashSet<Transaction>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<NotificationId> NotificationIds { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
