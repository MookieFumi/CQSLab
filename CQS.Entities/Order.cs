using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQS.Entities
{
    public class Order
    {
        public virtual int OrderId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool IsPaid { get; set; }
        public decimal Amount { get; set; }
    }
}
