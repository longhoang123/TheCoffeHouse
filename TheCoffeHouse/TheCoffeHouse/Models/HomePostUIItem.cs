using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheCoffeHouse.Models
{
    public class HomePostUIItem : BindableBase
    {
        public string Post1ID { get; set; }
        public string Title1 { get; set; }
        public string Image1 { get; set; }
        public DateTime Date1 { get; set; }
        public string Url1 { get; set; }
        public string Post2ID { get; set; }
        public string Title2 { get; set; }
        public string Image2 { get; set; }
        public DateTime Date2 { get; set; }
        public string Url2 { get; set; }
        private bool _isEvenNumber = true;

        public bool IsEvenNumber
        {
            get => _isEvenNumber;
            set => SetProperty(ref _isEvenNumber, value); 
        }

    }
}
