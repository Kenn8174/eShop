using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class OrderLine
    {
        public int PhoneID { get; set; }
        public int OrderID { get; set; }
        
        public Phone Phone { get; set; }
        public Order Order { get; set; }
    }
}
