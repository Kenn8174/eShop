using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Adresse { get; set; }
        public string Password { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
