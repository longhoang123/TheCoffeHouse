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
using TheCoffeHouse.Renderers.ToastMessage;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;
using TheCoffeHouse.Views.Popups;
using Xamarin.Forms;

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
            OpenAddressPageCommand = new DelegateCommand(OpenAddressPageExec);
            ChooseCodeCouponCommand = new DelegateCommand(ChooseCodeCouponExec);
            DeleteCouponCommand = new DelegateCommand(DeleteCouponExec);
            initData();
        }
        int Discount = 0;
        int Shipping = 0;
        Cart cart;
        async void initData()
        {
            // init isAtStore is true
            ConstaintVaribles.isAtStore = true;
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
            //Discount = 0;
            isDirectMoney = true;
            isAtStore = true;
            isDirect = true;

            if(ConstaintVaribles.Coupon != null)
            {
                Discount = ConstaintVaribles.Coupon.CouponDiscount;
                CouponImage = ConstaintVaribles.Coupon.CouponImage;
                Code = ConstaintVaribles.Coupon.Code;
                CouponDiscount = ConstaintVaribles.Coupon.CouponDiscount;
                hasCoupon = true;
            }
            ConstaintVaribles.TotalPrice = cart.TotalPrice;
            TotalPriceCart = ConstaintVaribles.TotalPrice - ConstaintVaribles.TotalPrice * Discount / 100 + Shipping;

            if (ConstaintVaribles.Store != null)
            {
                StoreName = ConstaintVaribles.Store.StoreName.ToString();
                StoreImage = ConstaintVaribles.Store.StoreImage;
            }
            else
            {
                StoreName = "Vui lòng chọn cửa hàng";
            }
            if(ConstaintVaribles.Address != null)
            {
                AddressUser = ConstaintVaribles.Address.AddressString;
            }
            else
            {
                AddressUser = "Vui lòng chọn địa chỉ nhận hàng";
            }
            if(ConstaintVaribles.isAtStore == true)
            {
                isAtStore = true;
                isAtHome = false;
            }
            else
            {
                isAtStore = false;
                isAtHome = true;
            }
         
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            Coupon SelectedCoupon = new Coupon();
          
            if (parameters != null && parameters.Keys.Count() > 0)
            {
                if (parameters.TryGetValue(ParamKey.StoreSelected.ToString(), out SelectedStore))
                {
                    ConstaintVaribles.Store = SelectedStore;
                    ConstaintVaribles.isAtStore = true;
                }

                SelectedCoupon = parameters.GetValue<Coupon>(ParamKey.CouponSelected.ToString());
                if(SelectedCoupon != null)
                {
                    ConstaintVaribles.Coupon = SelectedCoupon;
                    Discount = SelectedCoupon.CouponDiscount;
                    CouponImage = SelectedCoupon.CouponImage;
                    Code = SelectedCoupon.Code;
                    CouponDiscount = SelectedCoupon.CouponDiscount;
                    hasCoupon = true;
                }
                else
                {
                    Discount = 0;
                    hasCoupon = false;
                }
                if (parameters.TryGetValue(ParamKey.SelectedAddress.ToString(), out SelectedAddress))
                {
                    ConstaintVaribles.Address = SelectedAddress;
                    ConstaintVaribles.isAtStore = false;
                }
            }    
        }
        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);

        }
        #region Properties
        Store SelectedStore = new Store();
        Address SelectedAddress = new Address();
        private int _CouponDiscount;

        public int CouponDiscount
        {
            get { return _CouponDiscount; }
            set { SetProperty(ref _CouponDiscount, value); }
        }

        private string _Code;

        public string Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }

        private string _CouponImage;

        public string CouponImage
        {
            get { return _CouponImage; }
            set { SetProperty(ref _CouponImage, value); }
        }

        private string _StoreName;

        public string StoreName
        {
            get { return _StoreName; }
            set { SetProperty(ref _StoreName, value); }
        }
        private string _StoreImage;

        public string StoreImage
        {
            get { return _StoreImage; }
            set { SetProperty(ref _StoreImage, value); }
        }

        private bool _hasCoupon = false;

        public bool hasCoupon
        {
            get { return _hasCoupon; }
            set
            {
                SetProperty(ref _hasCoupon, value);
            }
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

        private string _AddressUser;

        public string AddressUser
        {
            get { return _AddressUser; }
            set { SetProperty(ref _AddressUser, value); }
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
                if (ConstaintVaribles.Store == null)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn cửa hàng để nhận sản phẩm", "OK");
                    //DependencyService.Get<Toast>().Show("Vui lòng chọn cửa hàng để nhận sản phẩm");
                    isChecked = false;
                }
                else
                {
                    order.IDStore = ConstaintVaribles.Store.IDStore;
                    order.StoreName = ConstaintVaribles.Store.StoreName;
                    order.StoreAddress = ConstaintVaribles.Store.StoreAddress;
                    isChecked = true;
                }
            }
            else
            {
                if(ConstaintVaribles.Address == null)
                {
                    //DependencyService.Get<Toast>().Show("Vui lòng chọn địa chỉ của bạn để nhận sản phẩm");
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn địa chỉ của bạn để nhận sản phẩm", "OK");
                    isChecked = false;
                }
                else
                {
                    methodReceive = "Nhận tận tay";
                    order.Shipping = 30000;
                    order.AddressUser = AddressUser;
                    isChecked = true;
                }               
            }

            order.IDCart = ConstaintVaribles.IDCart;
            order.IDUser = Convert.ToInt32(ConstaintVaribles.UserID);
            order.UserName = UserName;
            order.PhoneNumber = PhoneNumber;
            order.Discount = Discount;

            order.StatusOrder = "Đã đặt hàng";
            order.PaymentMethod = methodPayment;
            order.DeliveryMethod = "Chuyển nhanh";
            order.DateOrder = DateTime.Now;
            if (isChecked)
            {
                await LoadingPopup.Instance.Show();
                var ordered = await ApiService.CreateOrder(order);
              
                var user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
                int CurrentBean = 0;
                if (user != null)
                {
                    CurrentBean = Convert.ToInt32(user.Bean);
                    CurrentBean += TotalPriceCart * 1 / 1000;
                    await ApiService.UpdateUserBean(user.UserID, CurrentBean);
                    ConstaintVaribles.user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
                    HomeTabPageViewModel.instance.setisLogin();
                    CollectPointPageViewModel.instance.inituser();
                }
                OrderPageViewModel.instance.initQty();
                await LoadingPopup.Instance.Hide();
                DependencyService.Get<Toast>().Show("Tạo đơn hàng thành công");
                //await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn đã đặt hàng thành công", "OK");
                await Navigation.NavigateAsync($"../../../{PageManagement.HistoryPage}");
                OrderPageViewModel.instance.initRecentlyDrink();
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
        #region OpenAddressPageCommand
        public ICommand OpenAddressPageCommand { get; set; }
        private void OpenAddressPageExec()
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
            Navigation.NavigateAsync(PageManagement.AddressPage, navParam);
        }
        #endregion
        #region ChooseCodeCoupon
        public ICommand ChooseCodeCouponCommand { get; set; }
        private void ChooseCodeCouponExec()
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
            Navigation.NavigateAsync(PageManagement.AllCouponPage, navParam);
        }
        #endregion
        #region DeleteCouponCommand
        public ICommand DeleteCouponCommand { get; set; }
        private void DeleteCouponExec()
        {
            ConstaintVaribles.Coupon = null;
            hasCoupon = false;
            Discount = 0;
            TotalPriceCart = cart.TotalPrice - cart.TotalPrice * Discount / 100 + Shipping;

        }
        #endregion
    }
}
