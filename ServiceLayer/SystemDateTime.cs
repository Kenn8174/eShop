using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
