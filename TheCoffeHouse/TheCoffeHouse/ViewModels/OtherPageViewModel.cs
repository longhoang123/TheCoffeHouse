using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
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
        }

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
        #endregion
    }
}
