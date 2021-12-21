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
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class CartPageViewModel : BaseViewModel
    {
        public CartPageViewModel(
              INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Giỏ hàng";
            ListDetailCart = new ObservableCollection<DetailCart>();
            DeleteItemCartCommand = new DelegateCommand<DetailCart>(selectedItem => DeleteItemCartExec(selectedItem));
            OpenPaymentPageCommand = new DelegateCommand(OpenPaymentPageExec);

        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            initData();
        }
        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);
            initData();
        }
        async void initData()
        {
            Cart cart = new Cart();
            if (ConstaintVaribles.IDCart != 0)
            {
                ListDetailCart = await ApiService.GetAllDetailCart(ConstaintVaribles.IDCart);
            }
            if(ConstaintVaribles.UserID != null)
            {
                cart = await ApiService.GetCartByIDUser(Convert.ToInt32(ConstaintVaribles.UserID));
            }
            TotalPriceCart = cart.TotalPrice;
            if(TotalPriceCart == 0 || ListDetailCart.Count == 0)
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
            
        }
        #region Properties
        private bool _isEmpty;

        public bool isEmpty
        {
            get { return _isEmpty; }
            set {
                SetProperty(ref _isEmpty, value);
                RaisePropertyChanged("isEmpty");
            }
        }

        private int _TotalPriceCart;

        public int TotalPriceCart
        {
            get { return _TotalPriceCart; }
            set {
                SetProperty(ref _TotalPriceCart, value);
                RaisePropertyChanged("TotalPriceCart");
            }
        }

        private ObservableCollection<DetailCart> _listDetailCart;

        public ObservableCollection<DetailCart> ListDetailCart
        {
            get { return _listDetailCart; }
            set
            {
                SetProperty(ref _listDetailCart, value);
                RaisePropertyChanged("ListDetailCart");
            }
        }

        #endregion
        #region DeleteItemCartCommand
        public ICommand DeleteItemCartCommand { get; set; }

        public async void DeleteItemCartExec(DetailCart selectedItem)
        {
            var itemDeleted =  await ApiService.DeleteItemCart(selectedItem.IDDetailCart);
            if(itemDeleted != null)
            {
                initData();
                await App.Current.MainPage.DisplayAlert("Thông báo", $"Đã xóa sản phẩm {selectedItem.NameItem} khỏi giỏ hàng ", "OK");
            }

        }
        #endregion
        #region OpenPaymentPageCommand
        public ICommand OpenPaymentPageCommand { get; set; }
        public void OpenPaymentPageExec()
        {
            Navigation.NavigateAsync(PageManagement.PaymentPage);
        }
        #endregion
    }
}
