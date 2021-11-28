using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

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
            initcategory();
        }

        public class Category
        {
            public string TitleCategory { get; set; }
            public string ImageCategory { get; set; }
        }
        private void initcategory()
        {
            for(var i=0; i <= 8; i++)
            {
                ListCategory.Add(new Category { TitleCategory = "Tất cả", ImageCategory = "giftbox.png" });
            }
        }
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
    }
}
