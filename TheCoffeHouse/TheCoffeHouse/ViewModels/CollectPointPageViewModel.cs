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
        }
        private void initcoupon()
        {
            for (var i = 0; i <= 2; i++)
            {
                ListCoupon.Add(new Coupon { Title = "-Giảm giá 15% tất cả sản phẩm mua từ ngày 11/11/2021", Image = "coupon15.jpg", Date="Hết hạn sau 10 ngày"});
                ListPromotion.Add(new Promotion { Brand = "Coolmate", Description = "Ưu đãi đến 100k", NumPoint = "99", Image = "coolmate.jpg" });
            }
        }
        #region OpenPage
        public ICommand OpenAllCouponPage { get; set; }
        private async void OpenAllCouponPageExcute()
        {
            //string history = "Your history";
            //NavigationParameters navParams = new NavigationParameters();
            //navParams.Add(ParamKey.Username.ToString(), history);
            await Navigation.NavigateAsync(PageManagement.AllCouponPage);
        }
        #endregion
        #region ListCoupon
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
        #endregion


        #region ListPromotion
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

    }
}
