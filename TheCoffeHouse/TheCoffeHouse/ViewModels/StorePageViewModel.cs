using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class StorePageViewModel : BaseViewModel
    {
        public StorePageViewModel(
             INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService
            )
        {
            PageTitle = "Cửa hàng";
            ListStore = new ObservableCollection<Store>();
            initData();

        }
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
        }
        void initData()
        {
            ListStore.Add(new Store { IDStore = 1, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 2, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 3, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 4, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 5, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });
            ListStore.Add(new Store { IDStore = 6, StoreName = "THE COFFEE HOUSE", StoreImage = "coffeeHouse", StoreAddress = "86 Cao Thắng", StoreDistance = "cách đây 0.01km" });

        }
        #region Properties
        private ObservableCollection<Store> _listStore;

        public ObservableCollection<Store> ListStore
        {
            get { return _listStore; }
            set {
                SetProperty(ref _listStore, value);               
            }
        }

        #endregion
    }
}
