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
        public string CouponImage { get; set; }
        public string CouponBigImage { get; set; }
        public string CouponDate { get; set; }
    }
}
