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
            SizeLb.SetBinding(Button.TextProperty, new Binding("Size", source: this));
        }
        public static readonly BindableProperty SizeProperty =
               BindableProperty.Create("Size", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string Size
        {
            get { return (string)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly BindableProperty PriceTextProperty =
                BindableProperty.Create("PriceText", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string PriceText
        {
            get { return (string)GetValue(PriceTextProperty); }
            set { SetValue(PriceTextProperty, value); }
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
        //public static readonly BindableProperty ImageSourceProperty =
        //     BindableProperty.Create("Source", typeof(ImageSource), typeof(CustomCouponFrame), default(ImageSource));
        //public ImageSource Source
        //{
        //    get { return (ImageSource)GetValue(ImageSourceProperty); }
        //    set { SetValue(ImageSourceProperty, value); }
        //}
        public static readonly BindableProperty SourceProperty =
             BindableProperty.Create("Source", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}