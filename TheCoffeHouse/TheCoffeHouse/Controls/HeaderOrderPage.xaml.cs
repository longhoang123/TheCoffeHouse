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
    }
}
