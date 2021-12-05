﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheCoffeHouse.Models
{
    class User
    {
        public int IDUser { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Avatar { get; set; }
    }
}