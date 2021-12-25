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
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class SearchDrinkPageViewModel : BaseViewModel
    {
        public SearchDrinkPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Tìm kiếm";
            ListDrinks = new ObservableCollection<Drink>();
            SearchCommand = new DelegateCommand(SearchCommandExcute);
            InitData();
        }
        #region Function
        private async void InitData()
        {
            ObservableCollection<Drink> ListDrinksTmp = new ObservableCollection<Drink>();
                ListDrinksTmp = await ApiService.GetDrink();
                for (int j = 0; j < ListDrinksTmp.Count; j++)
                {
                    int ID = ListDrinksTmp[j].IDDrink;
                    ObservableCollection<DrinkImage> images = new ObservableCollection<DrinkImage>();
                    images = await ApiService.GetDrinkImageById(ID);
                    if (images.Count > 0)
                    {
                        ListDrinksTmp[j].DrinkImage = images[0].ImageData;
                    }
                }
            ListDrinks = ListDrinksTmp;
        }

        public ICommand SearchCommand { get; set; }
        private async void SearchCommandExcute()
        {
            ObservableCollection<Drink> ListDrinksTmp = new ObservableCollection<Drink>();
            ListDrinksTmp = await ApiService.GetDrinkByKey(SearchKey);
            for (int j = 0; j < ListDrinksTmp.Count; j++)
            {
                int ID = ListDrinksTmp[j].IDDrink;
                ObservableCollection<DrinkImage> images = new ObservableCollection<DrinkImage>();
                images = await ApiService.GetDrinkImageById(ID);
                if (images.Count > 0)
                {
                    ListDrinksTmp[j].DrinkImage = images[0].ImageData;
                }
            }
            ListDrinks = ListDrinksTmp;
        }
        #endregion
        #region Properties
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
        private string _searchKey;
        public string SearchKey
        {
            get { return _searchKey; }
            set
            {
                SetProperty(ref _searchKey, value);
            }
        }
        private Drink _selectedDrink;

        public Drink SelectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                if (_selectedDrink != value)
                {
                    SetProperty(ref _selectedDrink, value);
                    RaisePropertyChanged("SelectedDrink");
                    handleSelectedItem();
                }
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
