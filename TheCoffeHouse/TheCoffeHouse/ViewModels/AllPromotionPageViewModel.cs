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
    public class AllPromotionPageViewModel : BaseViewModel
    {
        public AllPromotionPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListPromotion = new ObservableCollection<Promotion>();
            initpromotion();
            OpenDetailPromotionPage = new DelegateCommand(OpenDetailPromotionPageExcute);
        }

        private void initpromotion()
        {
            for (var i = 0; i <= 7; i++)
            {
                ListPromotion.Add(new Promotion { Brand = "Coolmate", Description = "Ưu đãi đến 100k", NumPoint = "99", Image = "coolmate.jpg" });
            }
        }
        public ICommand OpenDetailPromotionPage { get; set; }
        private async void OpenDetailPromotionPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.DetailPromotionPage);
        }
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
