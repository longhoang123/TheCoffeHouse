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
    public class AllCouponPageViewModel : BaseViewModel
    {
        public AllCouponPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListCoupon = new ObservableCollection<Coupon>();
            initcoupon();
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            if (parameters != null && parameters.Keys.Count() > 0)
            {
                isNavFromPayment = parameters.GetValue<bool>(ParamKey.IsNavigateFromPaymentPage.ToString());
            }
        }
        bool isNavFromPayment = false;
        private async void OpenDetailCouponPageExcute()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("CouponSelected", SelectedCoupon);
            if (isNavFromPayment)
            {
                navParams.Add(ParamKey.IsNavigateFromPaymentPage.ToString(), true);
            }
            await Navigation.NavigateAsync(PageManagement.DetailCouponPage, navParams);
        }

        private async void initcoupon()
        {
            ListCoupon = await ApiService.GetCoupons();
        }
        #region ListCoupon
        private ObservableCollection<Coupon> _listCoupon;
        public ObservableCollection<Coupon> ListCoupon
        {
            get => _listCoupon;
            set
            {
                SetProperty(ref _listCoupon, value);
                RaisePropertyChanged("ListCoupon");
            }
        }
        #endregion
        #region Selecteditem
        private Coupon _selectedCoupon;
        public Coupon SelectedCoupon
        {
            get => _selectedCoupon;
            set
            {
                if (_selectedCoupon != value)
                {
                    SetProperty(ref _selectedCoupon, value);
                    RaisePropertyChanged("SelectedPromotion");
                    OpenDetailCouponPageExcute();
                }
            }
        }
        #endregion


    }
}
