using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class Medicine
    {
        public Medicine()
        {
            NotificationIds = new HashSet<NotificationId>();
            Transactions = new HashSet<Transaction>();
        }

        public int MedicineId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<NotificationId> NotificationIds { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
