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
            PromotionInfo = new Promotion();
        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            Promotion promotion = parameters.GetValue<Promotion>("PromotionSelected");
            //_promotion = promotion;
            //PromotionInfo = promotion;
        }
        private Promotion _promotion;
        public Promotion PromotionInfo
        {
            get => _promotion;
            set
            {
                if(_promotion!=null)
                {
                    SetProperty(ref _promotion, value);
                    RaisePropertyChanged("PromotionSelected");
                }
               
            }
        }
    }
}
