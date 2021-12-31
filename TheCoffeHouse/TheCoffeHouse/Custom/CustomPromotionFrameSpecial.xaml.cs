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
    public partial class CustomPromotionFrameSpecial : ContentView
    {
        public CustomPromotionFrameSpecial()
        {
            InitializeComponent();
            ImageButton.SetBinding(Image.SourceProperty, new Binding("SourceImage", source: this));
            Name.SetBinding(Label.TextProperty, new Binding("NameText", source: this));
            Size.SetBinding(Label.TextProperty, new Binding("SizeText", source: this));
            NumPoint.SetBinding(Button.TextProperty, new Binding("NumPointText", source: this));
            
        }
        public static readonly BindableProperty NumPointTextProperty =
                BindableProperty.Create("NumPointText", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string NumPointText
        {
            get { return (string)GetValue(NumPointTextProperty); }
            set { SetValue(NumPointTextProperty, value); }
        }

        public static readonly BindableProperty SizeTextProperty =
           BindableProperty.Create("SizeText", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string SizeText
        {
            get { return (string)GetValue(SizeTextProperty); }
            set { SetValue(SizeTextProperty, value); }
        }

        public static readonly BindableProperty NameTextProperty =
            BindableProperty.Create("NameText", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string NameText
        {
            get { return (string)GetValue(NameTextProperty); }
            set { SetValue(NameTextProperty, value); }
        }
        public static readonly BindableProperty SourceImageProperty =
             BindableProperty.Create("SourceImage", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string SourceImage
        {
            get { return (string)GetValue(SourceImageProperty); }
            set { SetValue(SourceImageProperty, value); }
        }
    }
}