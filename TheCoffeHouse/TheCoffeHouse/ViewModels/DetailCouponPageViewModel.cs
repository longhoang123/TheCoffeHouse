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
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            SelectedCoupon = parameters.GetValue<Coupon>("CouponSelected");
            Tilte = SelectedCoupon.Title;
        }

        private string _title;

        public string Tilte
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
                RaisePropertyChanged("Title");
            }
        }
        private Coupon _selectedCoupon;
        public Coupon SelectedCoupon
        {
            get { return _selectedCoupon; }
            set
            {
                SetProperty(ref _selectedCoupon, value);
                RaisePropertyChanged("SelectedCoupon");
            }
        }
    }
}
