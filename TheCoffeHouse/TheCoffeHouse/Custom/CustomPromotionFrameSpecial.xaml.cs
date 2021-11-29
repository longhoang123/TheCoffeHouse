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
            ImageButton.SetBinding(Image.SourceProperty, new Binding("Source", source: this));
            Name.SetBinding(Label.TextProperty, new Binding("NameText", source: this));
            Size.SetBinding(Label.TextProperty, new Binding("SizeText", source: this));
            NumPoint.SetBinding(Button.TextProperty, new Binding("NumPointText", source: this));
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this, EventArgs.Empty);
                    if (Command != null)
                    {
                        if (Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);
                    }
                })
            });
        }
        public static readonly BindableProperty NumPointTextProperty =
                BindableProperty.Create("NumPointText", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string NumPointText
        {
            get { return (string)GetValue(NumPointTextProperty); }
            set { SetValue(NumPointTextProperty, value); }
        }

        public static readonly BindableProperty DesTextProperty =
           BindableProperty.Create("SizeText", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string SizeText
        {
            get { return (string)GetValue(DesTextProperty); }
            set { SetValue(DesTextProperty, value); }
        }

        public static readonly BindableProperty BrandTextProperty =
            BindableProperty.Create("NameText", typeof(string), typeof(CustomPromotionFrameSpecial), default(string));
        public string NameText
        {
            get { return (string)GetValue(BrandTextProperty); }
            set { SetValue(BrandTextProperty, value); }
        }
        public event EventHandler Clicked;
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomPromotionFrameSpecial), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomPromotionFrameSpecial), null);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly BindableProperty ImageSourceProperty =
             BindableProperty.Create("Source", typeof(ImageSource), typeof(CustomPromotionFrameSpecial), default(ImageSource));
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}