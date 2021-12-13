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
    public partial class CustomCouponFrame : ContentView
    {
        public CustomCouponFrame()
        {
            InitializeComponent();
            ImageButton.SetBinding(Image.SourceProperty, new Binding("SourceImage", source: this));
            TitleCoupon.SetBinding(Label.TextProperty, new Binding("TitleText", source: this));
            DateCoupon.SetBinding(Label.TextProperty, new Binding("DateText", source: this));
        }

        public static readonly BindableProperty DateTextProperty =
           BindableProperty.Create("DateText", typeof(string), typeof(CustomCouponFrame), default(string));
        public string DateText
        {
            get { return (string)GetValue(DateTextProperty); }
            set { SetValue(DateTextProperty, value); }
        }

        public static readonly BindableProperty TitleTextProperty =
            BindableProperty.Create("TitleText", typeof(string), typeof(CustomCouponFrame), default(string));
        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }
        public event EventHandler Clicked;
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomCouponFrame), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomCouponFrame), null);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly BindableProperty SourceImageProperty =
             BindableProperty.Create("SourceImage", typeof(string), typeof(CustomCouponFrame), default(string));
        public string SourceImage
        {
            get { return (string)GetValue(SourceImageProperty); }
            set { SetValue(SourceImageProperty, value); }
        }
    }
}