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
    public partial class ContentPageHeaderView : ContentView
    {
        public ContentPageHeaderView()
        {
            InitializeComponent();
            labelTitle.SetBinding(Label.TextProperty, new Binding
            {
                Path = nameof(Title),
                Source = this,
                Mode = BindingMode.TwoWay
            });
            imageRight.SetBinding(Image.SourceProperty, new Binding
            {
                Path = nameof(RightIcon),
                Source = this,
                Mode = BindingMode.TwoWay
            });

            imageRight.GestureRecognizers.Add(new TapGestureRecognizer
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
           typeof(ContentPageHeaderView),
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

        #region RightIcon
        public static readonly BindableProperty RightIconProperty
       = BindableProperty.Create(nameof(RightIcon),
           typeof(string),
           typeof(ContentPageHeaderView),
           defaultValue: string.Empty,
           defaultBindingMode: BindingMode.TwoWay);



        public string RightIcon
        {
            get
            {
                return GetValue(RightIconProperty)?.ToString();
            }
            set
            {
                SetValue(RightIconProperty, value);
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