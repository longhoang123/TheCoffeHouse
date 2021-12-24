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
    public class StoreDetailPageViewModel : BaseViewModel
    {
        public StoreDetailPageViewModel(
                         INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService

            )
        {
            ChooseStoreCommand = new DelegateCommand(ChooseStoreExec);
        }
        public override async void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            if (parameters != null && parameters.Keys.Count() > 0)
            {
                isNavFromPayment = parameters.GetValue<bool>(ParamKey.IsNavigateFromPaymentPage.ToString());
            }
            SelectedStore = parameters.GetValue<Store>("StoreSelected");           
            Store_Add = SelectedStore.StoreAddress;
            Store_Name = SelectedStore.StoreName;
            ListImage = await ApiService.GetImageByIDStore(SelectedStore.IDStore);

        }
        #region Properties

        private ObservableCollection<StoreImage> _ListImage;

        public ObservableCollection<StoreImage> ListImage
        {
            get { return _ListImage; }
            set {
                SetProperty(ref _ListImage, value);
            }
        }

        private bool isNavFromPayment = false;
        private string _Store_Name;

        public string Store_Name
        {
            get { return _Store_Name; }
            set { SetProperty(ref _Store_Name, value); }
        }

        private string _Store_Add;

        public string Store_Add
        {
            get { return _Store_Add; }
            set { SetProperty(ref _Store_Add, value); }
        }

        private string _ImgStore;

        public string ImgStore
        {
            get { return _ImgStore; }
            set { SetProperty(ref _ImgStore, value); }
        }
        private Store _SelectedStore;

        public Store SelectedStore
        {
            get { return _SelectedStore; }
            set { SetProperty(ref _SelectedStore, value); }
        }

        #endregion
        #region ChooseStoreCommand
        public ICommand ChooseStoreCommand { get; set; }
        private async void ChooseStoreExec()
        {
            if (isNavFromPayment)
            {
                NavigationParameters navParams = new NavigationParameters();
                navParams.Add(ParamKey.StoreSelected.ToString(), SelectedStore);
                await Navigation.NavigateAsync($"../../../{PageManagement.PaymentPage}", navParams);
            }
        }
        #endregion
    }
}
