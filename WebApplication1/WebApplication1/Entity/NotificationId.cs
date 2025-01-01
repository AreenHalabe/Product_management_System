using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class NotificationId
    {
        public int NotificationId1 { get; set; }
        public int? MedicineId { get; set; }
        public int SupplierId { get; set; }
        public int AdminId { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool Status { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
