using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Phone> Phone { get; set; }
    }
}
