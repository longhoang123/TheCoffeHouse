using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class Cart
    {
        public int IDCart { get; set; }
        public int IDUser { get; set; }
        public int QuantityItem { get; set; }
        public int TotalPrice { get; set; }
    }
}
