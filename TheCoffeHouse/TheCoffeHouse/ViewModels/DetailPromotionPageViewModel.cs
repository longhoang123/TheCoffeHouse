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
    public class DetailPromotionPageViewModel : BaseViewModel
    {
        public DetailPromotionPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {

        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            SelectedPromotion = parameters.GetValue<Promotion>("PromotionSelected");
            TitlePromotion = SelectedPromotion.PromotionDes;
            ImagePromotion = SelectedPromotion.PromotionImage;
            NumPointPromotion =SelectedPromotion.Point;
        }
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
    }
}
