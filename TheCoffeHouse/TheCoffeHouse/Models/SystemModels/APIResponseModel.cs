using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoffeHouse.Models.SystemModels
{
    public class JObjectResponseModel
    {
        public bool Error { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public JObject Result { get; set; }
    }

}
