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
            TitleCoupon = SelectedCoupon.Title;
            DateCoupon = SelectedCoupon.CouponDate;
            ImageCoupon = SelectedCoupon.CouponImage;
            CodeCoupon = SelectedCoupon.Code;
        }

        private string _titleCoupon;

        public string TitleCoupon
        {
            get { return _titleCoupon; }
            set
            {
                SetProperty(ref _titleCoupon, value);
                RaisePropertyChanged("Title");
            }
        }
        private string _imageCoupon;

        public string ImageCoupon
        {
            get { return _imageCoupon; }
            set
            {
                SetProperty(ref _imageCoupon, value);
                RaisePropertyChanged("Image");
            }
        }
        private string _dateCoupon;

        public string DateCoupon
        {
            get { return _dateCoupon; }
            set
            {
                SetProperty(ref _dateCoupon, value);
                RaisePropertyChanged("Date");
            }
        }

        private string _codeCoupon;
        public string CodeCoupon
        {
            get { return _codeCoupon; }
            set
            {
                SetProperty(ref _codeCoupon, value);
                RaisePropertyChanged("Code");
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
