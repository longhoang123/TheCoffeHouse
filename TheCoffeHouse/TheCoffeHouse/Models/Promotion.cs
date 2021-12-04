using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheCoffeHouse.Models
{
    public class Promotion
    {
        public string Brand { get; set; }
        public string Description { get; set; }
        public string NumPoint { get; set; }
        public ImageSource Image { get; set; }
        public ImageSource BigImage { get; set; }

    }
}
