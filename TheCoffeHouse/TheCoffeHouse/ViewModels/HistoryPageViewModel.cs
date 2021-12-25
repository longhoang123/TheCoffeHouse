using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class HistoryPageViewModel : BaseViewModel
    {
        public HistoryPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListOrder = new ObservableCollection<Order>();
            PageTitle = "Lịch sử đơn hàng";
            instance = this;
            init();
        }
        public static HistoryPageViewModel instance;
        #region ListHistoryInit
        //Thêm danh sách lịch sử order từ db
        public async void init()
        {
            if(ConstaintVaribles.UserID != null)
            {
                ListOrder = await ApiService.GetAllOrderByIduser(Convert.ToInt32(ConstaintVaribles.UserID));
            }

        }
        #endregion
        #region Properties
        private ObservableCollection<Order> _listOrder;
        public ObservableCollection<Order> ListOrder
        {
            get => _listOrder;
            set
            {
                SetProperty(ref _listOrder, value);
                RaisePropertyChanged("Order");
            }
        }
        #endregion

        #region Selecteditem
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder != value)
                {
                    SetProperty(ref _selectedOrder, value);
                    RaisePropertyChanged("SelectedOrder");
                    OpenDetailOrder();
                }
            }
        }
        private async void OpenDetailOrder()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("OrderSelected", SelectedOrder);
            await Navigation.NavigateAsync(PageManagement.DetailOrderPage, navParams);
        }
        #endregion
    }
}
