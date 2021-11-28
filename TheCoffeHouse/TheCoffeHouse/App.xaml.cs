using Prism;
using Prism.Ioc;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels;
using TheCoffeHouse.Views;
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

            await NavigationService.NavigateAsync($"NavigationPage/{PageManagement.ExchangePromotionPage}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Page1, Page1ViewModel>(PageManagement.Page1);
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
        }
    }
}
