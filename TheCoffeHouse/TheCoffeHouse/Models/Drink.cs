using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class Drink
    {
        public int IDDrink { get; set; }
        public int IDCate { get; set; }
        public string DrinkName { get; set; }
        public int DrinkPrice { get; set; }
        //public ObservableCollection<string> DrinkImage { get; set; }
        public string DrinkImage { get; set; }
        public string Size { get; set; }
        public string DrinkDescription { get; set; }
    }
}
