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



namespace TheCoffeHouse.ViewModels
{
    public class PaymentPageViewModel : BaseViewModel
    {
        public PaymentPageViewModel(
            INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Thanh toán";

            OrderCommand = new DelegateCommand(OrderExec);
            OpenStorePageCommand = new DelegateCommand(OpenStorePageExec);
            initData();
        }
        int Discount = 0;
        int Shipping = 0;
        Cart cart;
        async void initData()
        {
            var user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
            UserName = user.Name;
            PhoneNumber = user.Phone;
            cart = await ApiService.GetCartByIDUser(Convert.ToInt32(ConstaintVaribles.UserID));
            if (cart.TotalPrice != 0)
            {
                isEnable = true;
            }
            else
            {
                isEnable = false;
            }
            TotalPrice = cart.TotalPrice;
            QuantityItem = cart.QuantityItem;
            isDiscount = false;
            Discount = 0;
            isDirectMoney = true;
            isAtStore = true;
            isDirect = true;
            TotalPriceCart = cart.TotalPrice;
            if(ConstaintVaribles.IDStore != 0)
            {
                StoreName = ConstaintVaribles.IDStore.ToString();
            }
            else
            {
                StoreName = SelectedStore.StoreName;
            }
         
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            SelectedStore = new Store { StoreName = "Chọn cửa hàng", StoreAddress = "Chọn cửa hàng để đến lấy" };
            base.OnNavigatedNewTo(parameters);
            if (parameters != null && parameters.Keys.Count() > 0)
            {
                SelectedStore = parameters.GetValue<Store>(ParamKey.StoreSelected.ToString()) ?? new Store {StoreName="Chọn cửa hàng", StoreAddress = "Chọn cửa hàng để đến lấy" };
                ConstaintVaribles.IDStore = SelectedStore.IDStore;
            }
            initData();
        }
        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);

        }
        #region Properties
        Store SelectedStore = new Store();

        private string _StoreName;

        public string StoreName
        {
            get { return _StoreName; }
            set { SetProperty(ref _StoreName, value); }
        }


        private bool _isEnable;

        public bool isEnable
        {
            get { return _isEnable; }
            set
            {
                SetProperty(ref _isEnable, value);
            }
        }

        private int _TotalPriceCart;

        public int TotalPriceCart
        {
            get { return _TotalPriceCart; }
            set
            {
                SetProperty(ref _TotalPriceCart, value);
            }
        }


        private bool _isDirect;

        public bool isDirect
        {
            get { return _isDirect; }
            set
            {
                SetProperty(ref _isDirect, value);
            }
        }

        private bool _isAtStore;

        public bool isAtStore
        {
            get { return _isAtStore; }
            set
            {
                SetProperty(ref _isAtStore, value);
                isDirect = false;

                if (_isAtStore == true)
                {
                    Shipping = 0;
                    TotalPriceCart = cart.TotalPrice - cart.TotalPrice * Discount / 100 + Shipping;
                }

            }
        }

        private bool _isAtHome;

        public bool isAtHome
        {
            get { return _isAtHome; }
            set
            {
                SetProperty(ref _isAtHome, value);
                isDirect = true;
                if (_isAtHome == true)
                {
                    Shipping = 30000;
                    TotalPriceCart = cart.TotalPrice - cart.TotalPrice * Discount / 100 + Shipping;
                }

            }
        }

        private bool _isDirectMoney;

        public bool isDirectMoney
        {
            get { return _isDirectMoney; }
            set
            {
                SetProperty(ref _isDirectMoney, value);

            }
        }
        private bool _isOnline;

        public bool isOnline
        {
            get { return _isOnline; }
            set
            {
                SetProperty(ref _isOnline, value);

            }
        }

        private string _DiscountCode;
        public string DiscountCode
        {
            get { return _DiscountCode; }
            set
            {
                if (_DiscountCode != value)
                {
                    SetProperty(ref _DiscountCode, value);
                    if (_DiscountCode == "CODE10%")
                    {
                        isDiscount = true;
                        StringDiscount = "Bạn được giảm giá 10%";
                        Discount = 10;

                    }
                    else
                    {
                        isDiscount = false;
                        Discount = 0;
                    }
                    TotalPriceCart = cart.TotalPrice - cart.TotalPrice * Discount / 100 + Shipping;
                }
            }
        }
        private string _StringDiscount;

        public string StringDiscount
        {
            get { return _StringDiscount; }
            set
            {
                SetProperty(ref _StringDiscount, value);
            }
        }

        private bool _isDiscount;

        public bool isDiscount
        {
            get { return _isDiscount; }
            set
            {
                SetProperty(ref _isDiscount, value);
            }
        }
        private int _QuantityItem;

        public int QuantityItem
        {
            get { return _QuantityItem; }
            set
            {
                SetProperty(ref _QuantityItem, value);
            }
        }

        private int _TotalPrice;

        public int TotalPrice
        {
            get { return _TotalPrice; }
            set
            {
                SetProperty(ref _TotalPrice, value);
            }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
            }
        }
        private string _phonenumber;

        public string PhoneNumber
        {
            get { return _phonenumber; }
            set
            {
                SetProperty(ref _phonenumber, value);
            }
        }


        #endregion
        #region OrderCommand
        public ICommand OrderCommand { get; set; }

        private async void OrderExec()
        {
            string methodPayment = "";
            string methodReceive = "";
            bool isChecked = true;
            Order order = new Order();
            if (isDirectMoney == true)
            {
                methodPayment = "Thanh toán bằng tiền mặt";
            }
            else
            {
                methodPayment = "Thanh toán online";
            }
            if (isAtStore == true)
            {
                methodReceive = "Nhận tại cửa hàng";
                order.Shipping = 0;
                if (ConstaintVaribles.IDStore == 0)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn cửa hàng để nhận sản phẩm", "OK");
                    isChecked = false;
                }
                else
                {
                    order.IDStore = ConstaintVaribles.IDStore;
                    isChecked = true;
                }
            }
            else
            {
                methodReceive = "Nhận tận tay";
                order.Shipping = 30000;

            }

            order.IDCart = ConstaintVaribles.IDCart;
            order.IDUser = Convert.ToInt32(ConstaintVaribles.UserID);

            order.Discount = Discount;

            order.StatusOrder = "Đã đặt hàng";
            order.PaymentMethod = methodPayment;
            order.DeliveryMethod = "Chuyển nhanh";
            order.DateOrder = DateTime.Now;
            if (isChecked)
            {
                await ApiService.CreateOrder(order);
                OrderPageViewModel.instance.initQty();
                await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn đã đặt hàng thành công", "OK");
                //App.instance.naviToOtherPage();
                await Navigation.NavigateAsync($"../../../{PageManagement.HistoryPage}");               
            }


        }

        #endregion
        #region OpenStorePageCommand
        public ICommand OpenStorePageCommand { get; set; }
        private void OpenStorePageExec()
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
            Navigation.NavigateAsync(PageManagement.StorePage, navParam);
        }
        #endregion
    }
}
