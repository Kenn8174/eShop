using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ShopService.DTO
{
    public class ShopDTO
    {
        public int PhoneID { get; set; }
        public string PhoneName { get; set; }
        public decimal Price { get; set; }
        public string CompanyName { get; set; }
        public string PhonePhoto { get; set; }

        public int PhotoID { get; set; }
    }
}
