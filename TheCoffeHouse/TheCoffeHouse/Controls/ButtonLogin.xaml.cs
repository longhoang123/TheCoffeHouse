using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheCoffeHouse.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonLogin : ContentView
    {
        public ButtonLogin()
        {
            InitializeComponent();
            label.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Text),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            label.SetBinding(Label.TextColorProperty, new Binding
            {
                Path = nameof(TextColor),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            image.SetBinding(Image.SourceProperty, new Binding
            {
                Path = nameof(ButtonImage),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            frame.SetBinding(Frame.BackgroundColorProperty, new Binding
            {
                Path = nameof(BackgroundColor),
                Source = this,
                Mode = BindingMode.TwoWay
            });


        }

       

        #region Text
        public static readonly BindableProperty TextProperty
       = BindableProperty.Create(nameof(Text),
           typeof(string),
           typeof(ButtonLogin),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string Text
        {
            get
            {
                return GetValue(TextProperty)?.ToString();
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        #endregion

        #region BackgroundColor
        public static readonly BindableProperty BackgroundColorProperty
       = BindableProperty.Create(nameof(BackgroundColor),
           typeof(Color),
           typeof(ButtonLogin),
           defaultValue: Color.Black,
           defaultBindingMode: BindingMode.TwoWay);



        public Color BackgroundColor
        {
            get
            {
                return  (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        #endregion

        #region TextColor
        public static readonly BindableProperty TextColorProperty
       = BindableProperty.Create(nameof(TextColor),
           typeof(Color),
           typeof(ButtonLogin),
           defaultValue: Color.Black,
           defaultBindingMode: BindingMode.TwoWay);



        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        #endregion


        #region ImageSource
        public static readonly BindableProperty ImageSourceProperty
       = BindableProperty.Create(nameof(ButtonImage),
           typeof(string),
           typeof(ButtonLogin),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string ButtonImage
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
    }
}