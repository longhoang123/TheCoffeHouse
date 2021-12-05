using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class Store
    {
        public int IDStore { get; set; }
        public string StoreName { get; set; }
        public string StoreImage { get; set; } // nào có data sửa lại
        public string StoreAddress { get; set; }
        public string StoreDistance { get; set; }
    }
}
