using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
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
            SelectedStoreCommand = new DelegateCommand<Store>(store => SelectedStoreExec(store));
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
        async void initData()
        {
            ObservableCollection<Store> storestmp = new ObservableCollection<Store>();
            storestmp = await ApiService.GetAllStore();
           
            for (int i = 0; i< storestmp.Count; i++)
            {
                ObservableCollection<StoreImage> storeImages = await ApiService.GetImageByIDStore(storestmp[i].IDStore);
                if(storeImages.Count > 0)
                {
                    storestmp[i].StoreImage = storeImages[0].StoreImageData;
                }
            }
            ListStore = storestmp;
        }
        #region Properties
        private bool isNavFromMain = false;
        private bool isNavFromPayment = false;
        //private Store _selectedStored;

        //public Store SelectedStored
        //{
        //    get { return _selectedStored; }
        //    set 
        //    { 
        //        if(_selectedStored != value)
        //        {
        //            SetProperty(ref _selectedStored, value);
        //            RaisePropertyChanged("SelectedStored");
        //            handleSelectedItem();
        //        }
        //    }
        //}

       

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
        //private async void handleSelectedItem()
        //{
        //    NavigationParameters navParams = new NavigationParameters();
        //    navParams.Add(ParamKey.StoreSelected.ToString(), SelectedStored);
        //    if (isNavFromPayment)
        //    {
        //        navParams.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
        //    }
        //    if(isNavFromMain)
        //    {
        //        await Navigation.GoBackAsync(navParams);
        //    } else
        //    {
        //        await Navigation.NavigateAsync(PageManagement.StoreDetailPage, navParams);

        //    }
        //}
        #endregion
        #region SelectedStoreCommand
        public ICommand SelectedStoreCommand { get; set; }
        private async void SelectedStoreExec(Store store)
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add(ParamKey.StoreSelected.ToString(), store);
            if (isNavFromPayment)
            {
                navParams.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
            }
            if (isNavFromMain)
            {
                await Navigation.GoBackAsync(navParams);
            }
            else
            {
                await Navigation.NavigateAsync(PageManagement.StoreDetailPage, navParams);

            }
        }
        #endregion
    }
}
