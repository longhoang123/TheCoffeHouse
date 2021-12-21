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
    public class PaymentPageViewModel : BaseViewModel
    {
        public PaymentPageViewModel(
            INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Thanh toán";
            ListStore = new ObservableCollection<Store>();
            OrderCommand = new DelegateCommand(OrderExec);
            initData();
        }
        int Discount = 0;
        int Shipping = 0;
        Cart cart;
         async void initData()
        {
            ListStore.Add(new Store { IDStore = 1, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 2, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 3, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 4, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            var user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
            UserName = user.Name;
            PhoneNumber = user.Phone;
            cart = await ApiService.GetCartByIDUser(Convert.ToInt32(ConstaintVaribles.UserID));
            if(cart.TotalPrice != 0)
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
           

        }
        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);
            initData();
        }
        #region Properties
        private bool _isEnable;

        public bool isEnable
        {
            get { return _isEnable; }
            set {
                SetProperty(ref _isEnable, value);
            }
        }

        private int _TotalPriceCart;

        public int TotalPriceCart
        {
            get { return _TotalPriceCart; }
            set {
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
                
                if(_isAtStore == true)
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
                if(_isAtHome == true)
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
                    if(_DiscountCode == "CODE10%")
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

        private ObservableCollection<Store> _listStore;

        public ObservableCollection<Store> ListStore
        {
            get { return _listStore; }
            set
            {
                SetProperty(ref _listStore, value);
            }
        }

        #endregion
        #region OrderCommand
        public ICommand OrderCommand { get; set; }

        private async void OrderExec()
        {
            string methodPayment = "";
            string methodReceive = "";
            Order order = new Order();
            if (isDirectMoney == true)
            {
                 methodPayment = "Thanh toán bằng tiền mặt";
            }
            else
            {
                 methodPayment = "Thanh toán online";
            }
           if(isAtStore == true)
            {
                methodReceive = "Nhận tại cửa hàng";
                order.Shipping = 0;
               
            }
            else
            {
                methodReceive = "Nhận tận tay";
                order.Shipping = 30000;
               
            }
           
            order.IDCart = ConstaintVaribles.IDCart;
            order.IDUser = Convert.ToInt32(ConstaintVaribles.UserID);
            order.IDStore = 1;
            order.Discount = Discount;
          
            order.StatusOrder = "Đã đặt hàng";
            order.PaymentMethod = methodPayment;
            order.DeliveryMethod = "Chuyển nhanh";
            order.DateOrder = DateTime.Now;
       
            await ApiService.CreateOrder(order);
            await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn đã đặt hàng thành công", "OK");
            await Navigation.NavigateAsync(PageManagement.HistoryPage);
            //await Navigation.NavigateAsync("NavigationPage/TabContainerPage?selectedTab=OtherPage");

        }

        #endregion
    }
}
