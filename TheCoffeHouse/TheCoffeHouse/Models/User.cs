using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheCoffeHouse.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public DateTime? Birthdate { get; set; } 
        public int? Gender { get; set; } 
        public int? Bean { get; set; }
        public int? Role { get; set; }
        public string Password { get; set; }
    }
}
