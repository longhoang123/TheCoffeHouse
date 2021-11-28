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
    public partial class HeaderView : ContentView
    {
        public HeaderView()
        {
            InitializeComponent();
            label.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Title),
                Source = this,
                Mode = BindingMode.TwoWay
            });
        }

        #region Text
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
    }
}