using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string AddressDetail { get; set; }
        public string Block { get; set; }
        public string Gate { get; set; }
        public string Note { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }

        [JsonIgnore]
        public string AddressString {
            get {

                var str = "";
                if(!string.IsNullOrEmpty(Gate)) {
                    str += $"Cổng {Gate}, ";
                }
                if (!string.IsNullOrEmpty(Block))
                {
                    str += $"Tòa {Block}, ";
                }
                str += AddressDetail;
                if (!string.IsNullOrEmpty(Note))
                {
                    str += $" ({Note})";
                }
                
                return str;

            }
        }
    }
}
