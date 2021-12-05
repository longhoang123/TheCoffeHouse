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
    public partial class HeaderViewOther : ContentView
    {
        public HeaderViewOther()
        {
            InitializeComponent();
            label.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Title),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            icon.SetBinding(Image.IsVisibleProperty, new Binding
            {
                Path = nameof(IsHaveIcon),
                Source = this,
                Mode = BindingMode.TwoWay
            });
        }

        #region Title
        public static readonly BindableProperty TitleProperty
       = BindableProperty.Create(nameof(Title),
           typeof(string),
           typeof(HeaderView),
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

        #region IsHaveIcon
        public static readonly BindableProperty IsHaveIconProperty
       = BindableProperty.Create(nameof(IsHaveIcon),
           typeof(bool),
           typeof(HeaderView),
           defaultValue: true,
           defaultBindingMode: BindingMode.TwoWay);



        public bool IsHaveIcon
        {
            get
            {
                return (bool)GetValue(IsHaveIconProperty);
            }
            set
            {
                SetValue(IsHaveIconProperty, value);
            }
        }

        #endregion
    }
}