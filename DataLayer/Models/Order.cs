using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }

        public ICollection<OrderLine> OrderLine { get; set; }
        public User Users { get; set; }
    }
}
