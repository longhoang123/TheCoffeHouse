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
    public partial class CustomSmallButton : ContentView
    {
        public CustomSmallButton()
        {
            InitializeComponent();
            ImageButton.SetBinding(Image.SourceProperty, new Binding("Source", source: this));
            TitleButton.SetBinding(Label.TextProperty, new Binding("ButtonText", source: this));
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
        public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create("ButtonText", typeof(string), typeof(CustomSmallButton), default(string));
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        public event EventHandler Clicked;
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomSmallButton), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomSmallButton), null);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly BindableProperty ImageSourceProperty =
             BindableProperty.Create("Source", typeof(ImageSource), typeof(CustomSmallButton), default(ImageSource));
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}