using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
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
            init();
            ShippingMethod = "Nhanh";
            PaymentMethod = "Thanh toán khi nhận hàng";
            Address = "Số 51, Ấp 2, Lương Phú, Giồng Trôm, Bến Tre";
            TotalPayment = "150.000 VNĐ";
        }
        private void init()
        {
            ListItemOrder.Add(new DetailOrder { IDOrder = 1, Quantity = 1, IDPro= 1, Price=14000 });
            ListItemOrder.Add(new DetailOrder { IDOrder = 1, Quantity = 2, IDPro = 1, Price = 14000 });
            ListItemOrder.Add(new DetailOrder { IDOrder = 1, Quantity = 1, IDPro = 1, Price = 14000 });
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            Order order = parameters.GetValue<Order>("OrderSelected");
            Orderinfo = order;
            
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
        private string _totalPayment;
        public string TotalPayment
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
