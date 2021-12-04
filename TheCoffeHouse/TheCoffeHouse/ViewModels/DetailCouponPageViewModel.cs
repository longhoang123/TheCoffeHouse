using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class DetailCouponPageViewModel : BaseViewModel
    {
        public DetailCouponPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            CouponInfo = new Coupon();
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            Coupon coupon = parameters.GetValue<Coupon>("CouponSelected");
            //_couponinfo = coupon;
            //CouponInfo = coupon;
        }
        private Coupon _couponinfo;
        public Coupon CouponInfo
        {
            get => _couponinfo;
            set
            {
                if (_couponinfo != null)
                {
                    SetProperty(ref _couponinfo, value);
                    RaisePropertyChanged("PromotionSelected");
                }

            }
        }
    }
}
