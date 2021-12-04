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
using Xamarin.Forms;

namespace TheCoffeHouse.ViewModels
{
    public class ExchangePromotionPageViewModel : BaseViewModel
    {
        public ExchangePromotionPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListCategory = new ObservableCollection<Category>();
            ListPromotion = new ObservableCollection<Promotion>();
            initcategory();
            OpenAllPromotionPage = new DelegateCommand(OpenAllPromotionPageExcute);
            OpenCollectPointPage = new DelegateCommand(OpenCollectPointPageExcute);
        }

        public class Category
        {
            public string TitleCategory { get; set; }
            public ImageSource ImageCategory { get; set; }
        }
        private void initcategory()
        {
            for(var i=0; i <= 3; i++)
            {
                ListCategory.Add(new Category { TitleCategory = "Tất cả", ImageCategory = "giftbox.png" });
                ListPromotion.Add(new Promotion { Brand = "Coolmate", Description = "Ưu đãi đến 100k", NumPoint = "99", Image = "coolmate.jpg" });
            }
        }


        #region OpenPage

        public ICommand OpenCollectPointPage { get; set; }
        private async void OpenCollectPointPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.CollectPointPage);
        }
        
        private async void OpenDetailPromotionPageExcute()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("PromotionSelected", SelectedPromotion);
            await Navigation.NavigateAsync(PageManagement.DetailPromotionPage, navParams);
        }


        public ICommand OpenAllPromotionPage { get; set; }
        private async void OpenAllPromotionPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AllPromotionPage);
        }
        #endregion

        private ObservableCollection<Category> _listCategory;

        public ObservableCollection<Category> ListCategory
        {
            get => _listCategory;
            set
            {
                SetProperty(ref _listCategory, value);
                RaisePropertyChanged("Tất cả");
            }
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
        #region Selecteditem
        private Promotion _selectedPromotion;
        public Promotion SelectedPromotion
        {
            get => _selectedPromotion;
            set
            {
                if (_selectedPromotion != value)
                {
                    SetProperty(ref _selectedPromotion, value);
                    RaisePropertyChanged("SelectedPromotion");
                    OpenDetailPromotionPageExcute();
                }
            }
        }
        #endregion
    }
}
