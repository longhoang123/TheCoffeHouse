using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    class Order
    {
        public int IDCart { get; set; }
        public int IDUser { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Point { get; set; }
        public int Shipping { get; set; }
        public int TotalPayment { get; set; }
    }
}
