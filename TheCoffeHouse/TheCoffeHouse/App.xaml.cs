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
            await NavigationService.NavigateAsync(PageManagement.OrderPage);
            //await NavigationService.NavigateAsync("NavigationPage/TabContainerPage?selectedTab=HomeTabPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(PageManagement.LoginPage);
            containerRegistry.RegisterForNavigation<TabContainerPage, TabContainerPageViewModel>(PageManagement.TabContainerPage);
            containerRegistry.RegisterForNavigation<HomeTabPage, HomeTabPageViewModel>(PageManagement.HomeTabPage);
            containerRegistry.RegisterForNavigation<OtherPage, OtherPageViewModel>(PageManagement.OtherPage);
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryPageViewModel>(PageManagement.HistoryPage);
            containerRegistry.RegisterForNavigation<PolicyPage, PolicyPageViewModel>(PageManagement.PolicyPage);
            containerRegistry.RegisterForNavigation<VotePage, VotePageViewModel>(PageManagement.VotePage);
            containerRegistry.RegisterForNavigation<ContactPage, ContactPageViewModel>(PageManagement.ContactPage);
            containerRegistry.RegisterForNavigation<PersonalInfoPage, PersonalInfoPageViewModel>(PageManagement.PersonalInfoPage);
            containerRegistry.RegisterForNavigation<AddressPage, AddressPageViewModel>(PageManagement.AddressPage);
            containerRegistry.RegisterForNavigation<SettingPage, SettingPageViewModel>(PageManagement.SettingPage);
            containerRegistry.RegisterForNavigation<CollectPointPage, CollectPointPageViewModel>(PageManagement.CollectPointPage);
            containerRegistry.RegisterForNavigation<ExchangePromotionPage, ExchangePromotionPageViewModel>(PageManagement.ExchangePromotionPage);            
            containerRegistry.RegisterForNavigation<OrderPage, OrderPageViewModel>(PageManagement.OrderPage);
            containerRegistry.RegisterForNavigation<AllCouponPage, AllCouponPageViewModel>(PageManagement.AllCouponPage);
            containerRegistry.RegisterForNavigation<DetailCouponPage, DetailCouponPageViewModel>(PageManagement.DetailCouponPage);
            containerRegistry.RegisterForNavigation<AllPromotionPage, AllPromotionPageViewModel>(PageManagement.AllPromotionPage);
            containerRegistry.RegisterForNavigation<DetailPromotionPage, DetailPromotionPageViewModel>(PageManagement.DetailPromotionPage);
            containerRegistry.RegisterForNavigation<StorePage, StorePageViewModel>();



            
        }
    }
}
