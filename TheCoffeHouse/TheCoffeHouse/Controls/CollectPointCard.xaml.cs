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
    public partial class CollectPointCard : ContentView
    {
        public CollectPointCard()
        {
            InitializeComponent();
            labelName.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Name),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            labelBean.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Bean),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            imageBarCode.SetBinding(Image.SourceProperty, new Binding
            {
                Path = nameof(BarCode),
                Source = this,
                Mode = BindingMode.TwoWay
            });
        }

        #region Name
        public static readonly BindableProperty NameProperty
       = BindableProperty.Create(nameof(Name),
           typeof(string),
           typeof(CollectPointCard),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string Name
        {
            get
            {
                return GetValue(NameProperty)?.ToString();
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }

        #endregion

        #region BarCode
        public static readonly BindableProperty BarCodeProperty
       = BindableProperty.Create(nameof(BarCode),
           typeof(string),
           typeof(CollectPointCard),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string BarCode
        {
            get
            {
                return GetValue(BarCodeProperty)?.ToString();
            }
            set
            {
                SetValue(BarCodeProperty, value);
            }
        }

        #endregion

        #region Bean
        public static readonly BindableProperty BeanProperty
       = BindableProperty.Create(nameof(Bean),
           typeof(string),
           typeof(CollectPointCard),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string Bean
        {
            get
            {
                return GetValue(BeanProperty)?.ToString();
            }
            set
            {
                SetValue(BeanProperty, value);
            }
        }

        #endregion
    }
}