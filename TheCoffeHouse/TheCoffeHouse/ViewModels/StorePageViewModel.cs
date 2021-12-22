using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class StorePageViewModel : BaseViewModel
    {
        public StorePageViewModel(
             INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService
            )
        {
            PageTitle = "Cửa hàng";
            ListStore = new ObservableCollection<Store>();
            initData();

        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            if (parameters != null && parameters.Keys.Count() > 0)
            {
                isNavFromMain = parameters.GetValue<bool>(ParamKey.IsNavigateFromMainPage.ToString());
                isNavFromPayment  = parameters.GetValue<bool>(ParamKey.IsNavigateFromPaymentPage.ToString());
            }
            base.OnNavigatedNewTo(parameters);
        }
        void initData()
        {
            ListStore.Add(new Store { IDStore = 1, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 2, StoreName = "THE COFFEE HOUSE 1", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 3, StoreName = "THE COFFEE HOUSE 2", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 4, StoreName = "THE COFFEE HOUSE 3", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 5, StoreName = "THE COFFEE HOUSE 4", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 6, StoreName = "THE COFFEE HOUSE 5", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });

        }
        #region Properties
        private bool isNavFromMain = false;
        private bool isNavFromPayment = false;
        private Store _selectedStored;

        public Store SelectedStored
        {
            get { return _selectedStored; }
            set 
            { 
                if(_selectedStored != value)
                {
                    SetProperty(ref _selectedStored, value);
                    RaisePropertyChanged("SelectedStored");
                    handleSelectedItem();
                }
            }
        }

       

        private ObservableCollection<Store> _listStore;

        public ObservableCollection<Store> ListStore
        {
            get { return _listStore; }
            set {
                SetProperty(ref _listStore, value);               
            }
        }

        #endregion
        #region selectedItemListview
        private async void handleSelectedItem()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add(ParamKey.StoreSelected.ToString(), SelectedStored);
            if (isNavFromPayment)
            {
                navParams.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
            }
            if(isNavFromMain)
            {
                await Navigation.GoBackAsync(navParams);
            } else
            {
                await Navigation.NavigateAsync(PageManagement.StoreDetailPage, navParams);

            }
        }
        #endregion
    }
}
