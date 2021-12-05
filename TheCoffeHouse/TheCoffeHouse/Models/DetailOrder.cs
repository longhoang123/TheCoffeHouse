using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    class DetailOrder
    {
        public int IDDetailOrder { get; set; }
        public int IDOrder { get; set; }
        public int IDPro { get; set; }
        public int Quantity { get; set; }
        public List<int> Price { get; set; }
        public string Size { get; set; }
        public int Total { get; set; }
    }
}
