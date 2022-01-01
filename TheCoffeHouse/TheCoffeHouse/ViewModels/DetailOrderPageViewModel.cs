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
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class DetailOrderPageViewModel : BaseViewModel
    {
        public DetailOrderPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListItemOrder = new ObservableCollection<DetailOrder>();
            PageTitle = "Chi tiết đơn hàng";
            CancelOrderCommand = new DelegateCommand(CancelOrderExec);
        }
        Order order;
        private async void init()
        {
            ListItemOrder = await ApiService.GetDetailOrderByIdOrder(order.IDOrder);
        }
        public override async void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            order = parameters.GetValue<Order>("OrderSelected");
            Orderinfo = order;
            ShippingMethod = order.DeliveryMethod;
            PaymentMethod = order.PaymentMethod;
            Address = "Số 51, Ấp 2, Lương Phú, Giồng Trôm, Bến Tre";
            TotalPayment =order.TotalPayment;
            var user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
            userName = user.Name;
            PhoneNumber = user.Phone;
            StatusOrder = order.StatusOrder;
            Discount = order.Discount.ToString();
            Shipping = order.Shipping.ToString();
            Point = order.Point.ToString();
            QuantityItem = order.QuantityItem;
            if(order.AddressUser != null)
            {
                isAtStore = false;
                AddressUser = order.AddressUser;
            }
            else
            {
                isAtStore = true;
                StoreName = order.StoreName;
                StoreAddress = order.StoreAddress;
            }
            if(order.StatusOrder== "Đã hủy đơn")
            {
                isCancelEnable = false;
            }
            else
            {
                isCancelEnable = true;
            }
            init();

        }
        private Order _order;
        public Order Orderinfo
        {
            get => _order;
            set
            {
                if (_order != null)
                {
                    SetProperty(ref _order, value);
                    RaisePropertyChanged("PromotionSelected");
                }

            }
        }
        #region Properties

        private string _StoreAddress;

        public string StoreAddress
        {
            get { return _StoreAddress; }
            set { SetProperty(ref _StoreAddress, value); }
        }

        private string _StoreName;

        public string StoreName
        {
            get { return _StoreName; }
            set { SetProperty(ref _StoreName, value); }
        }


        private string _AddressUser;

        public string AddressUser
        {
            get { return _AddressUser; }
            set { SetProperty(ref _AddressUser, value); }
        }


        private bool _isAtStore;

        public bool isAtStore
        {
            get { return _isAtStore; }
            set { SetProperty(ref _isAtStore, value); }
        }

        private int _quantityItem;

        public int QuantityItem
        {
            get { return _quantityItem; }
            set { SetProperty(ref _quantityItem, value); }
        }

        private string _point;

        public string Point
        {
            get { return _point; }
            set { SetProperty(ref _point, value); }
        }

        private string _Shipping;

        public string Shipping
        {
            get { return _Shipping; }
            set { SetProperty(ref _Shipping, value); }
        }

        private string _statusOrder;

        public string StatusOrder
        {
            get { return _statusOrder; }
            set { SetProperty(ref _statusOrder, value); }
        }
        private string _Discount;

        public string Discount
        {
            get { return _Discount; }
            set { SetProperty(ref _Discount, value); }
        }


        private ObservableCollection<DetailOrder> _listitemOrder;
        public ObservableCollection<DetailOrder> ListItemOrder
        {
            get => _listitemOrder;
            set
            {
                SetProperty(ref _listitemOrder, value);
                RaisePropertyChanged("Order");
            }
        }
     
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                SetProperty(ref _PhoneNumber, value);
                RaisePropertyChanged("PhoneNumber");
            }
        }

        private string _userName;
        public string userName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                RaisePropertyChanged("userName");
            }
        }
        private string _shippingMethod;
        public string ShippingMethod
        {
            get => _shippingMethod;
            set
            {
                SetProperty(ref _shippingMethod, value);
                RaisePropertyChanged("ShippingMethod");
            }
        }
        private string _paymentMethod;
        public string PaymentMethod
        {
            get => _paymentMethod;
            set
            {
                SetProperty(ref _paymentMethod, value);
                RaisePropertyChanged("PaymentMethod");
            }
        }
        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                SetProperty(ref _address, value);
                RaisePropertyChanged("Address");
            }
        }
        private int _totalPayment;
        public int TotalPayment
        {
            get => _totalPayment;
            set
            {
                SetProperty(ref _totalPayment, value);
                RaisePropertyChanged("TotalPayment");
            }
        }
        private bool _isCancelEnable = true ;

        public bool isCancelEnable
        {
            get { return _isCancelEnable; }
            set { SetProperty(ref _isCancelEnable, value); }
        }

        #endregion
        #region CancelOrderCommand
        public ICommand CancelOrderCommand { get; set; }
        private async void CancelOrderExec()
        {
            var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có muốn hủy đơn hàng này?", "Ok", "Cancel");
            if (res)
            {
               var result =  await ApiService.UpdateStatusOrder(order.IDOrder, "Đã hủy đơn");
                if(result != null)
                {
                    StatusOrder = result.StatusOrder;
                    isCancelEnable = false;
                    HistoryPageViewModel.instance.init();
                    var user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
                    int CurrentBean = 0;
                    if (user != null)
                    {
                        CurrentBean = Convert.ToInt32(user.Bean);
                        CurrentBean -= order.Point;
                        await ApiService.UpdateUserBean(user.UserID, CurrentBean);
                        ConstaintVaribles.user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
                        HomeTabPageViewModel.instance.setisLogin();
                        CollectPointPageViewModel.instance.inituser();
                    }
                }
            }
        }
        #endregion
    }
}
