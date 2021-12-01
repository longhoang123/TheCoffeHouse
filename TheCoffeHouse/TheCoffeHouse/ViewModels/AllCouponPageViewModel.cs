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
    public class AllCouponPageViewModel : BaseViewModel
    {
        public AllCouponPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListCoupon = new ObservableCollection<Coupon>();
            initcoupon();
            OpenDetailCouponPage = new DelegateCommand(OpenDetailCouponPageExcute);
        }

        public ICommand OpenDetailCouponPage { get; set; }
        private async void OpenDetailCouponPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.DetailCouponPage);
        }

        private void initcoupon()
        {
            for (var i = 0; i <= 7; i++)
            {
                ListCoupon.Add(new Coupon { Title = "-Giảm giá 15% tất cả sản phẩm mua từ ngày 11/11/2021", Image = "coupon15.jpg", Date = "Hết hạn sau 10 ngày" });
            }
        }
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
        
    }
}
