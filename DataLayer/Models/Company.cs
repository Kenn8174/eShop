using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        [Display(Name = "Firma navn")]
        [StringLength(maximumLength: 30)]
        [Required]
        public string CompanyName { get; set; }

        public ICollection<Phone> Phone { get; set; }
    }
}
