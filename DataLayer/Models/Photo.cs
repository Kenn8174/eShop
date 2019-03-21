using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public int PhoneID { get; set; }
        public byte[] PhonePhoto { get; set; }

        public Phone Phone { get; set; }
    }
}
