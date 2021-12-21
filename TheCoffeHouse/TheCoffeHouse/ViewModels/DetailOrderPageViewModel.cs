using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        #endregion
    }
}
