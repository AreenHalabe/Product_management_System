using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int? MedicineId { get; set; }
        public int? SupplierId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
        public bool TransactionType { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
