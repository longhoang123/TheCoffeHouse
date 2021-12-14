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
    public class CartPageViewModel : BaseViewModel
    {
        public CartPageViewModel(
              INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Giỏ hàng";
            ListDetailCart = new ObservableCollection<DetailCart>();
            initData();
        }
        void initData()
        {
            ListDetailCart.Add(new DetailCart { IDDetailCart = 1, IDCart = 1, IDDrink = 1, NameItem = "Cà phê sữa đá", PriceItem = 50000, Quantity = 1, Size = "Nhỏ", Total = 50000, Image = "coffee_cup.jpg" });
            ListDetailCart.Add(new DetailCart { IDDetailCart = 1, IDCart = 1, IDDrink = 1, NameItem = "Cà phê sữa đá", PriceItem = 50000, Quantity = 1, Size = "Nhỏ", Total = 50000, Image = "coffee_cup.jpg" });
            ListDetailCart.Add(new DetailCart { IDDetailCart = 1, IDCart = 1, IDDrink = 1, NameItem = "Cà phê sữa đá", PriceItem = 50000, Quantity = 1, Size = "Nhỏ", Total = 50000, Image = "coffee_cup.jpg" });
            ListDetailCart.Add(new DetailCart { IDDetailCart = 1, IDCart = 1, IDDrink = 1, NameItem = "Cà phê sữa đá", PriceItem = 50000, Quantity = 1, Size = "Nhỏ", Total = 50000, Image = "coffee_cup.jpg" });
        }
        #region Properties
        private ObservableCollection<DetailCart> _listDetailCart;

        public ObservableCollection<DetailCart> ListDetailCart
        {
            get { return _listDetailCart; }
            set
            {
                SetProperty(ref _listDetailCart, value);
                RaisePropertyChanged("ListDetailCart");
            }
        }

        #endregion
    }
}
