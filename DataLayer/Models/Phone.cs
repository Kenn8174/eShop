using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class Phone
    {
        public int PhoneID { get; set; }
        public int CompanyID { get; set; }

        [StringLength(maximumLength: 30)]
        [Display(Name = "Telefon navn")]
        [Required]
        public string PhoneName { get; set; }

        [Display(Name = "Pris")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        public Company Company { get; set; }
        public Photo Photo { get; set; }
        public ICollection<OrderLine> OrderLine { get; set; }
    }
}
