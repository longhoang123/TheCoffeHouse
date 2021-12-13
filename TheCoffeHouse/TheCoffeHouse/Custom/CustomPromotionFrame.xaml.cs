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
    public partial class CustomPromotionFrame : ContentView
    {
        public CustomPromotionFrame()
        {
            InitializeComponent();
            ImageButton.SetBinding(Image.SourceProperty, new Binding("SourceImage", source: this));
            Brand.SetBinding(Label.TextProperty, new Binding("BrandText", source: this));
            Description.SetBinding(Label.TextProperty, new Binding("DesText", source: this));
            NumPoint.SetBinding(Button.TextProperty, new Binding("NumPointText", source: this));
           
        }
        public static readonly BindableProperty NumPointTextProperty =
                BindableProperty.Create("NumPointText", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string NumPointText
        {
            get { return (string)GetValue(NumPointTextProperty); }
            set { SetValue(NumPointTextProperty, value); }
        }

        public static readonly BindableProperty DesTextProperty =
           BindableProperty.Create("DesText", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string DesText
        {
            get { return (string)GetValue(DesTextProperty); }
            set { SetValue(DesTextProperty, value); }
        }

        public static readonly BindableProperty BrandTextProperty =
            BindableProperty.Create("BrandText", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string BrandText
        {
            get { return (string)GetValue(BrandTextProperty); }
            set { SetValue(BrandTextProperty, value); }
        }
        public event EventHandler Clicked;
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomPromotionFrame), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomPromotionFrame), null);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly BindableProperty SourceImageProperty =
             BindableProperty.Create("SourceImage", typeof(string), typeof(CustomPromotionFrame), default(string));
        public string SourceImage
        {
            get { return (string)GetValue(SourceImageProperty); }
            set { SetValue(SourceImageProperty, value); }
        }
    }
}