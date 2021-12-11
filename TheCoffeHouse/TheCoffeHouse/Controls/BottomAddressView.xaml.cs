using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheCoffeHouse.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomAddressView : ContentView
    {
        public BottomAddressView()
        {
            InitializeComponent();
            label.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Title),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            labelAddress.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Address),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            image.SetBinding(Image.SourceProperty, new Binding
            {
                Path = nameof(ImageSource),
                Source = this,
                Mode = BindingMode.TwoWay
            });
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

        #region Title
        public static readonly BindableProperty TitleProperty
       = BindableProperty.Create(nameof(Title),
           typeof(string),
           typeof(BottomAddressView),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string Title
        {
            get
            {
                return GetValue(TitleProperty)?.ToString();
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        #endregion

        #region Address
        public static readonly BindableProperty AddressProperty
       = BindableProperty.Create(nameof(Address),
           typeof(string),
           typeof(BottomAddressView),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string Address
        {
            get
            {
                return GetValue(AddressProperty)?.ToString();
            }
            set
            {
                SetValue(AddressProperty, value);
            }
        }

        #endregion

        #region ImageSource
        public static readonly BindableProperty ImageSourceProperty
       = BindableProperty.Create(nameof(ImageSource),
           typeof(string),
           typeof(BottomAddressView),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string ImageSource
        {
            get
            {
                return GetValue(ImageSourceProperty)?.ToString();
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        #endregion
        #region Command
        public event EventHandler Clicked;
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ContentPageHeaderView), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ContentPageHeaderView), null);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

    }
}