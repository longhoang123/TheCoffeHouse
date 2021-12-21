﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class Order
    {
        public int IDOrder { get; set; }
        public int IDCart { get; set; }
        public int IDUser { get; set; }
        public int IDStore { get; set; }
        public int Total { get; set; }
        public int QuantityItem { get; set; }
        public int Discount { get; set; }
        public int Point { get; set; }
        public int Shipping { get; set; }
        public string StatusOrder { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalPayment { get; set; }
        public string Address { get; set; }
        public string AddressShop { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime DateOrder { get; set; }
    }
}
