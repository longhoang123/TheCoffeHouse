using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;
using TheCoffeHouse.Views.Popups;
using Xamarin.Essentials;

namespace TheCoffeHouse.ViewModels
{
    public class DetailPromotionPageViewModel : BaseViewModel
    {
        public DetailPromotionPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ChangePromotion = new DelegateCommand(ChangePromotionExcute);
        }
        public override async void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            SelectedPromotion = parameters.GetValue<Promotion>("PromotionSelected");
            TitlePromotion = SelectedPromotion.PromotionDes;
            ImagePromotion = SelectedPromotion.PromotionImage;
            NumPointPromotion =SelectedPromotion.Point;
            PromotionEnable = false;
            var user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
            if(user!=null)
            {
                User = user;
            }
            if (User != null)
            {
                if (User.Bean >= Convert.ToInt32(NumPointPromotion))
                {
                    PromotionEnable = true;
                }
            }    
        }
        #region Properties
        private string _titlePromotion;

        public string TitlePromotion
        {
            get { return _titlePromotion; }
            set
            {
                SetProperty(ref _titlePromotion, value);
                RaisePropertyChanged("Title");
            }
        }

        private string _imagePromotion;

        public string ImagePromotion
        {
            get { return _imagePromotion; }
            set
            {
                SetProperty(ref _imagePromotion, value);
                RaisePropertyChanged("Image");
            }
        }
        private string _numPointPromotion;

        public string NumPointPromotion
        {
            get { return _numPointPromotion; }
            set
            {
                SetProperty(ref _numPointPromotion, value);
                RaisePropertyChanged("NumPoint");
            }
        }

        private Promotion _selectedPromotion;
        public Promotion SelectedPromotion
        {
            get { return _selectedPromotion; }
            set
            {
                SetProperty(ref _selectedPromotion, value);
                RaisePropertyChanged("SelectedPromotion");
            }
        }

        private bool _promotionEnable;
        public bool PromotionEnable
        {
            get { return _promotionEnable; }
            set
            {
                SetProperty(ref _promotionEnable, value);
                RaisePropertyChanged("PromotionEnable");
            }
        }
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                SetProperty(ref _user, value);
            }
        }

        #endregion
        #region ChangePromotion
        public ICommand ChangePromotion { get; set; }
        private async void ChangePromotionExcute()
        {
            int CurrentBean = 0;
            CurrentBean= Convert.ToInt32(User.Bean) - Convert.ToInt32(NumPointPromotion);
            await ApiService.UpdateUserBean(User.UserID, CurrentBean);
            ConstaintVaribles.user = await ApiService.GetUserByID(Convert.ToInt32(ConstaintVaribles.UserID));
            await DetailPromotionPopup.Instance.Show(SelectedPromotion.Brand, SelectedPromotion.PromotionCode, SelectedPromotion.PromotionDiscount.ToString(), SelectedPromotion.PromotionDes);
            HomeTabPageViewModel.instance.setisLogin();
            CollectPointPageViewModel.instance.inituser();
            SendMailExtension.SendEmail("Mã giảm giá","Chúc mừng bạn đã nhận được một mã giảm giá\n"+ "Mã giảm giá của bạn là: " + SelectedPromotion.PromotionCode+"\n"+SelectedPromotion.PromotionDes, "nguyenlong2k14@gmail.com");
            await Navigation.NavigateAsync(PageManagement.CollectPointPage);
        }
        #endregion
        
    }
}
