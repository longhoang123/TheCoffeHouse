using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class Drink
    {
        public int IDDrink { get; set; }
        public string DrinkName { get; set; }
        public int DrinkPrice { get; set; }
        public string DrinkImage { get; set; }
        public string Size { get; set; }
    }
}
