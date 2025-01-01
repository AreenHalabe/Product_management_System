using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class Customer
    {
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
