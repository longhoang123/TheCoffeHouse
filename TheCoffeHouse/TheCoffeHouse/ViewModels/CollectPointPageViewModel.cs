using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class CollectPointPageViewModel : BaseViewModel
    {
        public CollectPointPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListCoupon = new ObservableCollection<Coupon>();
            ListPromotion = new ObservableCollection<Promotion>();
            initcoupon();
            OpenAllCouponPage = new DelegateCommand(OpenAllCouponPageExcute);
            OpenExchangePromotionPage = new DelegateCommand(OpenExchangePromotionPageExcute);
            OpenHistoryPage = new DelegateCommand(OpenHistoryPageExcute);
        }
        private void initcoupon()
        {
            for (var i = 0; i <= 2; i++)
            {
                ListCoupon.Add(new Coupon {Code="TCF15", Title = "-Giảm giá 15% tất cả sản phẩm mua từ ngày 11/11/2021", Image = "coupon15.jpg", Date="Hết hạn sau 10 ngày"});
                ListPromotion.Add(new Promotion { Brand = "Coolmate", Description = "Ưu đãi đến 100k", NumPoint = "99", Image = "coolmate.jpg" });
            }
        }

        #region OpenPage
        public ICommand OpenHistoryPage { get; set; }
        private async void OpenHistoryPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.HistoryPage);
        }

        private async void OpenDetailPromotionPageExcute()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("PromotionSelected", SelectedPromotion);
            await Navigation.NavigateAsync(PageManagement.DetailPromotionPage, navParams);
        }

        public ICommand OpenExchangePromotionPage { get; set; }
        private async void OpenExchangePromotionPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.ExchangePromotionPage);
        }

        private async void OpenDetailCouponPageExcute()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("CouponSelected", SelectedCoupon);
            await Navigation.NavigateAsync(PageManagement.DetailCouponPage, navParams);
        }
        public ICommand OpenAllCouponPage { get; set; }
        private async void OpenAllCouponPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AllCouponPage);
        }



        #endregion
        #region List
        private ObservableCollection<Coupon> _listCoupon;
        public ObservableCollection<Coupon> ListCoupon
        {
            get => _listCoupon;
            set
            {
                SetProperty(ref _listCoupon, value);
                RaisePropertyChanged("-Giảm giá 15% tất cả sản phẩm mua từ ngày 11/11/2021");
            }
        }

        private ObservableCollection<Promotion> _listPromotion;
        public ObservableCollection<Promotion> ListPromotion
        {
            get => _listPromotion;
            set
            {
                SetProperty(ref _listPromotion, value);
                RaisePropertyChanged("-Giảm giá 15% tất cả sản phẩm mua từ ngày 11/11/2021");
            }
        }
        #endregion

        #region Selecteditem
        private Promotion _selectedPromotion;
        public Promotion SelectedPromotion
        {
            get => _selectedPromotion;
            set
            {
                if(_selectedPromotion != value)
                {
                    SetProperty(ref _selectedPromotion, value);
                    RaisePropertyChanged("SelectedPromotion");
                    OpenDetailPromotionPageExcute();
                }
            }
        }

        private Coupon _selectedCoupon;
        public Coupon SelectedCoupon
        {
            get => _selectedCoupon;
            set
            {
                if (_selectedCoupon != value)
                {
                    SetProperty(ref _selectedCoupon, value);
                    RaisePropertyChanged("SelectedCoupon");
                    OpenDetailCouponPageExcute();
                }
            }
        }        
        #endregion


    }
}
