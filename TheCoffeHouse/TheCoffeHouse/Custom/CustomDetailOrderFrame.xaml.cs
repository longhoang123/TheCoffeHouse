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
    public partial class CustomDetailOrderFrame : ContentView
    {
        public CustomDetailOrderFrame()
        {
            InitializeComponent();
            Image.SetBinding(Image.SourceProperty, new Binding("Source", source: this));
            Name.SetBinding(Label.TextProperty, new Binding("NameText", source: this));
            QuantityLb.SetBinding(Label.TextProperty, new Binding("Quantity", source: this));
            PriceLb.SetBinding(Button.TextProperty, new Binding("PriceText", source: this));
        }
        public static readonly BindableProperty PriceProperty =
                BindableProperty.Create("Price", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly BindableProperty QuantityProperty =
           BindableProperty.Create("Quantity", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string Quantity
        {
            get { return (string)GetValue(QuantityProperty); }
            set { SetValue(QuantityProperty, value); }
        }

        public static readonly BindableProperty NameTextProperty =
            BindableProperty.Create("NameText", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string NameText
        {
            get { return (string)GetValue(NameTextProperty); }
            set { SetValue(NameTextProperty, value); }
        }
        public static readonly BindableProperty ImageSourceProperty =
             BindableProperty.Create("Source", typeof(ImageSource), typeof(CustomCouponFrame), default(ImageSource));
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}