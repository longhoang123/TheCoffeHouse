using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class DetailCart
    {
        //public int IDDetailCart { get; set; }
        //public int IDCart { get; set; }
        //public int IDPro { get; set; }
        //public int Quantity { get; set; }
        //public List<int> Price { get; set; }
        //public string Size { get; set; }
        //public int Total { get; set; }
        public int IDDetailCart { get; set; }
        public int IDCart { get; set; }
        public int IDDrink { get; set; }
        public string NameItem { get; set; }
        public int PriceItem { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public int Total { get; set; }
        public string Image { get; set; }
    }
}
