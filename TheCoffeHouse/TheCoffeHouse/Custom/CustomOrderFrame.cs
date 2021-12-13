using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheCoffeHouse.Custom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomOrderFrame : ContentView
    {
        public CustomOrderFrame()
        {
            InitializeComponent();
            Image.SetBinding(Image.SourceProperty, new Binding("Source", source: this));
            IDOrder.SetBinding(Label.TextProperty, new Binding("IDOrderText", source: this));
            Price.SetBinding(Label.TextProperty, new Binding("PriceText", source: this));
        }
        public static readonly BindableProperty PriceTextProperty =
                BindableProperty.Create("PriceText", typeof(string), typeof(CustomOrderFrame), default(string));
        public string PriceText
        {
            get { return (string)GetValue(PriceTextProperty); }
            set { SetValue(PriceTextProperty, value); }
        }
        public static readonly BindableProperty IDOrderTextProperty =
            BindableProperty.Create("IDOrderText", typeof(string), typeof(CustomOrderFrame), default(string));
        public string IDOrderText
        {
            get { return (string)GetValue(IDOrderTextProperty); }
            set { SetValue(IDOrderTextProperty, value); }
        }
        public static readonly BindableProperty ImageSourceProperty =
             BindableProperty.Create("Source", typeof(string), typeof(CustomOrderFrame), default(string));
        public string Source
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}