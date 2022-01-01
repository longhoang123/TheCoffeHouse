using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;
using TheCoffeHouse.Views.Popups;

namespace TheCoffeHouse.ViewModels
{
    public class AddressPageViewModel : BaseViewModel
    {
        public AddressPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            OpenAddAddressPage = new DelegateCommand(OpenAddAddressPageExcute);
            OpenHomeAddress = new DelegateCommand(OpenHomeAddressExcute);
            OpenCompanyAddress = new DelegateCommand(OpenCompanyAddressExcute);
            AddressSelectedCommand = new DelegateCommand<Address>(address  => AddressSelectedExecute(address));

            ListAddresses = new ObservableCollection<Address>();
        }

        #region Properties
        private ObservableCollection<Address> _listAddresses;

        public ObservableCollection<Address> ListAddresses
        {
            get { return _listAddresses; }
            set
            {
                SetProperty(ref _listAddresses, value);
                RaisePropertyChanged("ListAddresses");
            }
        }

        private ObservableCollection<Address> _listHomeAddresses;

        public ObservableCollection<Address> ListHomeAddresses
        {
            get { return _listHomeAddresses; }
            set
            {
                SetProperty(ref _listHomeAddresses, value);
                RaisePropertyChanged("ListHomeAddresses");
            }
        }

        private ObservableCollection<Address> _listCompanyAddresses;

        public ObservableCollection<Address> ListCompanyAddresses
        {
            get { return _listCompanyAddresses; }
            set
            {
                SetProperty(ref _listCompanyAddresses, value);
                RaisePropertyChanged("ListCompanyAddresses");
            }
        }

        private bool _isHomeAddressVisible = false;

        public bool IsHomeAddressVisible
        {
            get { return _isHomeAddressVisible; }
            set
            {
                SetProperty(ref _isHomeAddressVisible, value);
              
            }
        }

        private bool _isCompanyAddressVisible = false;

        public bool IsCompanyAddressVisible
        {
            get { return _isCompanyAddressVisible; }
            set
            {
                SetProperty(ref _isCompanyAddressVisible, value);

            }
        }
        #endregion


        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            LoadData();
        }

        public override async void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);
            var needRefresh = parameters.GetValue<bool>(ParamKey.NeedRefresh.ToString());
            if(needRefresh)
            {
                await LoadingPopup.Instance.Show();
                LoadData();
                await LoadingPopup.Instance.Hide();
            }
        }

        private async void LoadData()
        {
            ListAddresses = await ApiService.GetAddresses(int.Parse(ConstaintVaribles.UserID)) ?? new ObservableCollection<Address>();
            ListHomeAddresses = new ObservableCollection<Address>( ListAddresses.Where(x => x.Type == 0).ToList());
            ListCompanyAddresses = new ObservableCollection<Address>( ListAddresses.Where(x => x.Type == 1).ToList());
        }

        public ICommand OpenAddAddressPage { get; set; }
        private async void OpenAddAddressPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AddAddressPage);
        }


        public ICommand OpenHomeAddress { get; set; }
        private  void OpenHomeAddressExcute()
        {
            IsHomeAddressVisible = !IsHomeAddressVisible;
        }

        public ICommand OpenCompanyAddress { get; set; }
        private void OpenCompanyAddressExcute()
        {
            IsCompanyAddressVisible = !IsCompanyAddressVisible;
        }
        
        public ICommand AddressSelectedCommand { get; set; }
        private async void AddressSelectedExecute(Address address)
        {
            ConstaintVaribles.Address = address;
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add(ParamKey.SelectedAddress.ToString(), address);
            await Navigation.GoBackAsync(navParams);
        }
    }
}
