using Prism;
using Prism.Ioc;
using Prism.Navigation;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels;
using TheCoffeHouse.Views;
using TheCoffeHouse.Views.Tabs;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace TheCoffeHouse
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        #region Properties

        public static bool IsNotBusy
        {
            get => Preferences.Get(PreferencesKey.IsNotBusy.ToString(), true);
            set
            {
                Preferences.Set(PreferencesKey.IsNotBusy.ToString(), value);
            }
        }

        public SQLiteService SqLiteService { get; private set; }
        #endregion


        protected override async void OnInitialized()
        {
            InitializeComponent();
            //await NavigationService.NavigateAsync(PageManagement.LoginPage);
            await NavigationService.NavigateAsync("NavigationPage/TabContainerPage?selectedTab=HomeTabPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(PageManagement.LoginPage);

            containerRegistry.RegisterForNavigation<TabContainerPage, TabContainerPageViewModel>(PageManagement.TabContainerPage);
            containerRegistry.RegisterForNavigation<HomeTabPage, HomeTabPageViewModel>(PageManagement.HomeTabPage);
        }
    }
}
