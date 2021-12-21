using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class DetailOrder
    {
        public int IDDetailOrder { get; set; }
        public int IDOrder { get; set; }
        public int IDDrink { get; set; }
        public string NameItem { get; set; }
        public int PriceItem { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public int TotalPrice { get; set; }
        public string Image { get; set; }
    }
}
