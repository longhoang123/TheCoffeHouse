using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheCoffeHouse.Models
{
    public class Coupon
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public ImageSource Image { get; set; }
        public ImageSource BigImage { get; set; }
        public string Date { get; set; }
    }
}
