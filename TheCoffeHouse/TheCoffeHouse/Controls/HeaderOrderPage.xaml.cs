using Xamarin.Forms;

namespace TheCoffeHouse.Controls
{
    public partial class HeaderOrderPage : ContentView
    {
        public HeaderOrderPage()
        {
            InitializeComponent();
            label.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Title),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            qtyItemInCart.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Qtity),
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
        #region Qtity
        public static readonly BindableProperty QtityProperty
      = BindableProperty.Create(nameof(Qtity),
          typeof(string),
          typeof(HeaderView),
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.TwoWay);
        public string Qtity
        {
            get
            {
                return GetValue(QtityProperty)?.ToString();
            }
            set
            {
                SetValue(QtityProperty, value);
            }
        }
        #endregion
    }
}
