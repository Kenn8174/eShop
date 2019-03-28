using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Phone
    {
        public int PhoneID { get; set; }
        public int CompanyID { get; set; }
        public string PhoneName { get; set; }
        public decimal Price { get; set; }

        public Company Company { get; set; }
        public Photo Photo { get; set; }
        public ICollection<OrderLine> OrderLine { get; set; }
    }
}
