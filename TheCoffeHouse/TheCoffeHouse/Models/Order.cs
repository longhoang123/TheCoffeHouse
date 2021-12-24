using System;
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
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressUser { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public int Total { get; set; }
        public int QuantityItem { get; set; }
        public int Discount { get; set; }
        public int Point { get; set; }
        public int Shipping { get; set; }
        public string StatusOrder { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalPayment { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime DateOrder { get; set; }
    }
}
