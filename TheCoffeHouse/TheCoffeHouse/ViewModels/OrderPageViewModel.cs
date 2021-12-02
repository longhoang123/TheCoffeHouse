﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;
using Xamarin.Forms;

namespace TheCoffeHouse.ViewModels
{
    public class OrderPageViewModel : BaseViewModel
    {
        public OrderPageViewModel(
            INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Đặt hàng";
            ListDrinks = new ObservableCollection<Drink>();
            ListCategory = new ObservableCollection<string>();
            ListBanner = new ObservableCollection<Drink>();
            //ItemTappedCommand = new DelegateCommand(ItemTapped);
        }
        
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            InitData();

        }
        void InitData()
        {
            ListDrinks.Add(new Drink { IDDrink = 1, DrinkName = "Cà phê sữa đá", DrinkPrice = 50000, DrinkImage = "coffee_cup.jpg" });
            ListDrinks.Add(new Drink { IDDrink = 2, DrinkName = "Cà phê sữa nóng", DrinkPrice = 60000, DrinkImage = "coffee_cup.jpg" });
            ListDrinks.Add(new Drink { IDDrink = 3, DrinkName = "Cà phê đen", DrinkPrice = 70000, DrinkImage = "coffee_cup.jpg" });
            ListDrinks.Add(new Drink { IDDrink = 4, DrinkName = "Cà phê matcha", DrinkPrice = 40000, DrinkImage = "coffee_cup.jpg" });

            ListCategory.Add("Cà phê");
            ListCategory.Add("Trà trái cây");
            ListCategory.Add("Đá xay");
            ListCategory.Add("Bánh snack");
            ListCategory.Add("Combo");


            ListBanner.Add(new Drink { IDDrink = 1, DrinkName = "Cà phê sữa đá", DrinkPrice = 50000, DrinkImage = "Tradao.jpg" });
            ListBanner.Add(new Drink { IDDrink = 2, DrinkName = "Cà phê sữa nóng", DrinkPrice = 60000, DrinkImage = "Tradao.jpg" });
            ListBanner.Add(new Drink { IDDrink = 3, DrinkName = "Cà phê đen", DrinkPrice = 70000, DrinkImage = "Tradao.jpg" });
            ListBanner.Add(new Drink { IDDrink = 4, DrinkName = "Cà phê matcha", DrinkPrice = 40000, DrinkImage = "Tradao.jpg" });

        }



        #region Properties

        private Drink  _selectedDrink;

        public Drink SelectedDrink
        {
            get { return _selectedDrink; }
            set 
            { 
                if(_selectedDrink != value)
                {
                    SetProperty(ref _selectedDrink, value);
                    RaisePropertyChanged("SelectedDrink");
                    handleSelectedItem();
                }
            }
        }

        private ObservableCollection<Drink> _listBanner;

        public ObservableCollection<Drink> ListBanner
        {
            get { return _listBanner; }
            set
            {
                SetProperty(ref _listBanner, value);
                RaisePropertyChanged("ListBanner");
            }
        }

      

        private ObservableCollection<Drink> _listDrinks;

        public ObservableCollection<Drink> ListDrinks
        {
            get => _listDrinks;
            set
            {
                SetProperty(ref _listDrinks, value);
                RaisePropertyChanged("ListDrinks");
            }
        }

        private ObservableCollection<string> _listCategory;

        public ObservableCollection<string> ListCategory
        {
            get { return _listCategory; }
            set 
            {
                SetProperty(ref _listCategory, value);
                RaisePropertyChanged("ListCategory");
            }
        }
        #endregion
        #region selectedItemListview
        private async void handleSelectedItem()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("DrinkSelected", SelectedDrink);
            await Navigation.NavigateAsync(PageManagement.ProductDetailPage, navParams);
        }
        #endregion
    }
}
