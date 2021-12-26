using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class OtherPageViewModel : BaseViewModel
    {
        public OtherPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Page Other";
            OpenHistoryPage = new DelegateCommand(OpenHistoryPageExcute);
            OpenPolicyPage = new DelegateCommand(OpenPolicyPageExcute);
            OpenContactPage = new DelegateCommand(OpenContactPageExcute);
            OpenVotePage = new DelegateCommand(OpenVotePageExcute);
            OpenPersonalInfoPage = new DelegateCommand(OpenPersonalInfoPageExcute);
            OpenAddressPage = new DelegateCommand(OpenAddressPageExcute);
            OpenSettingPage = new DelegateCommand(OpenSettingPageExcute);
            OpenAdminPage = new DelegateCommand(OpenAdminPageExcute);
            Logout = new DelegateCommand(LogoutExcute);
        }

        #region Properties

        private static OtherPageViewModel _instance;
        public static OtherPageViewModel Instance => _instance ?? (_instance = new OtherPageViewModel());
        #endregion

        #region OpenPage
        public ICommand OpenHistoryPage { get; set; }
        private async void OpenHistoryPageExcute()
        {
            //string history = "Your history";
            //NavigationParameters navParams = new NavigationParameters();
            //navParams.Add(ParamKey.Username.ToString(), history);
            await Navigation.NavigateAsync(PageManagement.HistoryPage);
        }

        public ICommand OpenPolicyPage { get; set; }
        private async void OpenPolicyPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.PolicyPage);
        }

        public ICommand OpenContactPage { get; set; }
        private async void OpenContactPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.ContactPage);
        }

        public ICommand OpenVotePage { get; set; }
        private async void OpenVotePageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.VotePage);
        }
        public ICommand OpenPersonalInfoPage { get; set; }
        private async void OpenPersonalInfoPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.PersonalInfoPage);
        }
        public ICommand OpenAddressPage { get; set; }
        private async void OpenAddressPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AddressPage);
        }
        public ICommand OpenSettingPage { get; set; }
        private async void OpenSettingPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.SettingPage);
        }
        public ICommand OpenAdminPage { get; set; }
        private async void OpenAdminPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AdminMasterPage);
        }
        public ICommand Logout { get; set; }
        private async void LogoutExcute()
        {
            ConstaintVaribles.IsLogedIn = false;  
            ConstaintVaribles.UserID = null;
            ConstaintVaribles.user = null;
            ConstaintVaribles.IDStore = 0;
            ConstaintVaribles.Store = null;
            ConstaintVaribles.TotalPrice = 0;
            ConstaintVaribles.Coupon = null;
            ConstaintVaribles.IDCart = 0;
            HomeTabPageViewModel.instance.setisLogin();
            await Navigation.SelectTabAsync(PageManagement.HomeTabPage);
           
        }
        #endregion

        public override void OnAppear()
        {
            base.OnAppear();
            IsLogedin = ConstaintVaribles.IsLogedIn;
        }

        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);
            IsLogedin = ConstaintVaribles.IsLogedIn;
        }

    }
}
