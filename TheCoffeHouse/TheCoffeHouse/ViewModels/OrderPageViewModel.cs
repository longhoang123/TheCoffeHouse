using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
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
            //ListCategory = new ObservableCollection<string>();
            ListBanner = new ObservableCollection<Drink>();
            ListCategory = new ObservableCollection<Category>();
            ListCateDrinks = new ObservableCollection<CateDrink>();
            //ItemTappedCommand = new DelegateCommand(ItemTapped);
            SelectedCateCommand = new DelegateCommand<Category>(SelectedCate => SelectedCateCommandExcute(SelectedCate));
            SelectedDrinkCommand = new DelegateCommand<Drink>(SelectedDrink => SelectedDrinkCommandExcute(SelectedDrink));
            SelectedCategoryCommand = new DelegateCommand(SelectedCategoryExec);
            InitData();
            instance = this;

        }
        public static OrderPageViewModel instance;
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
        }


        async void InitData()
        {
            ObservableCollection<Drink> ListDrinksTmp = new ObservableCollection<Drink>();
            ObservableCollection<Category> ListCateTmp = new ObservableCollection<Category>();
            ListCategory = await ApiService.GetCategory();
            ListCateTmp = await ApiService.GetCategory();
            ListCategory.Add(new Category { IDCate = 9999, CateImage = "Coffee_cup.jpg", CateName = "Tất cả" });
            ObservableCollection<CateDrink> ListCateDrinksTmp = new ObservableCollection<CateDrink>();
            for (int i = 0; i < ListCateTmp.Count; i++)
            {
                int ID = ListCateTmp[i].IDCate;
                ListDrinksTmp = await ApiService.GetDrinkByCate(ID);
                for (int j = 0; j < ListDrinksTmp.Count; j++)
                {
                    ID = ListDrinksTmp[j].IDDrink;
                    ObservableCollection<DrinkImage> images = new ObservableCollection<DrinkImage>();
                    images = await ApiService.GetDrinkImageById(ID);
                    if (images.Count > 0)
                    {
                        ListDrinksTmp[j].DrinkImage = images[0].ImageData;
                    }
                }
                ListCateDrinksTmp.Add(new CateDrink { CategoryName = ListCateTmp[i].CateName, CategoryImage = ListCateTmp[i].CateImage, ListDrinkInfo = ListDrinksTmp });
            }
            ListCateDrinks = ListCateDrinksTmp;


            initQty();

            //ListBanner.Add(new Drink { IDDrink = 1, DrinkName = "Cà phê sữa đá", DrinkPrice = 50000, DrinkImage = "Tradao.jpg" });
            //ListBanner.Add(new Drink { IDDrink = 2, DrinkName = "Cà phê sữa nóng", DrinkPrice = 60000, DrinkImage = "Tradao.jpg" });
            //ListBanner.Add(new Drink { IDDrink = 3, DrinkName = "Cà phê đen", DrinkPrice = 70000, DrinkImage = "Tradao.jpg" });
            //ListBanner.Add(new Drink { IDDrink = 4, DrinkName = "Cà phê matcha", DrinkPrice = 40000, DrinkImage = "Tradao.jpg" });

        }
        #region MyRegion
        public  async void initRecentlyDrink()
        {
            if (ConstaintVaribles.UserID != null)
            {
                ObservableCollection<Drink> DrinkRecently = new ObservableCollection<Drink>();
                ObservableCollection<Order> orders = new ObservableCollection<Order>();
                orders = await ApiService.GetAllOrderByIduser(Convert.ToInt32(ConstaintVaribles.UserID)) ;
                if (orders != null)
                {
                    hasRecentlyDrink = true;
                    int check = 0;
                    foreach (Order ordertmp in orders)
                    {
                        var DetailOrders = await ApiService.GetDetailOrderByIdOrder(ordertmp.IDOrder);
                        if (DetailOrders != null)
                        {
                            foreach (DetailOrder detail in DetailOrders)
                            {
                                if (check < 5)
                                {
                                    var drinktmp = await ApiService.GetDrinkById(detail.IDDrink);
                                    if(DrinkRecently.Any(drink=>drink.IDDrink == drinktmp.IDDrink) == false)
                                    {
                                        DrinkRecently.Add(drinktmp);
                                        check++;
                                    } 
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    //ObservableCollection<Drink> drinktmps = new ObservableCollection<Drink>(DrinkRecently.Distinct());
                    for (int j = 0; j < DrinkRecently.Count; j++)
                    {
                        int ID = DrinkRecently[j].IDDrink;
                        ObservableCollection<DrinkImage> images = new ObservableCollection<DrinkImage>();
                        images = await ApiService.GetDrinkImageById(ID);
                        if (images.Count > 0)
                        {
                            DrinkRecently[j].DrinkImage = images[0].ImageData;
                        }
                    }
                    ListBanner = DrinkRecently;
                }
                else
                {
                    hasRecentlyDrink = false;
                }
            }
            else
            {
                hasRecentlyDrink = false;
            }
        }
        #endregion


        #region Properties

        private bool _hasRecentlyDrink = false;
        public bool hasRecentlyDrink
        {
            get { return _hasRecentlyDrink; }
            set { SetProperty(ref _hasRecentlyDrink, value); }
        }

        private int _Qtity;

        public int Qtity
        {
            get { return _Qtity; }
            set { SetProperty(ref _Qtity, value); }
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

        private Category _selectedCate;

        public Category SelectedCate
        {
            get { return _selectedCate; }
            set
            {
                if (_selectedCate != value)
                {
                    SetProperty(ref _selectedCate, value);
                    RaisePropertyChanged("SelectedCate");
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

        private ObservableCollection<Category> _listCategory;

        public ObservableCollection<Category> ListCategory
        {
            get { return _listCategory; }
            set
            {
                SetProperty(ref _listCategory, value);
                RaisePropertyChanged("ListCategory");
            }
        }

        private ObservableCollection<CateDrink> _listCateDrinks;

        public ObservableCollection<CateDrink> ListCateDrinks
        {
            get => _listCateDrinks;
            set
            {
                SetProperty(ref _listCateDrinks, value);
                RaisePropertyChanged("ListCateDrinks");
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

        #region SelectedCateCommand
        public ICommand SelectedCateCommand { get; set; }
        private async void SelectedCateCommandExcute(Category category)
        {
            ObservableCollection<Drink> ListDrinksTmp = new ObservableCollection<Drink>();
            if (category.CateName == "Tất cả")
            {
                ObservableCollection<Category> categories = await ApiService.GetCategory();
                ObservableCollection<CateDrink> ListCateDrinksTmp = new ObservableCollection<CateDrink>();
                for (int i = 0; i < categories.Count; i++)
                {
                    int ID = categories[i].IDCate;
                    ListDrinksTmp = await ApiService.GetDrinkByCate(ID);
                    for (int j = 0; j < ListDrinksTmp.Count; j++)
                    {
                        ID = ListDrinksTmp[j].IDDrink;
                        ObservableCollection<DrinkImage> images = new ObservableCollection<DrinkImage>();
                        images = await ApiService.GetDrinkImageById(ID);
                        if (images.Count > 0)
                        {
                            ListDrinksTmp[j].DrinkImage = images[0].ImageData;
                        }
                    }
                    ListCateDrinksTmp.Add(new CateDrink { CategoryName = categories[i].CateName, CategoryImage = categories[i].CateImage, ListDrinkInfo = ListDrinksTmp });
                }
                ListCateDrinks = ListCateDrinksTmp;
            }
            else if (category != null)
            {
                ObservableCollection<CateDrink> ListCateDrinksTmp = new ObservableCollection<CateDrink>();
                ListDrinksTmp = await ApiService.GetDrinkByCate(category.IDCate);
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
                ListCateDrinksTmp.Add(new CateDrink { CategoryName = category.CateName, CategoryImage = category.CateImage, ListDrinkInfo = ListDrinksTmp });
                ListCateDrinks = ListCateDrinksTmp;
            }
        }
        #endregion
        #region initQuantityItemInCart
        public async void initQty()
        {
            Qtity = 0;
            Cart cart = new Cart();
            if (ConstaintVaribles.UserID != null)
            {
                cart = await ApiService.GetCartByIDUser(Convert.ToInt32(ConstaintVaribles.UserID));
                Qtity = cart.QuantityItem;
            }
        }
        #endregion
        #region ModelCateDrink
        public class CateDrink
        {
            public string CategoryName { get; set; }
            public string CategoryImage { get; set; }
            public ObservableCollection<Drink> ListDrinkInfo { get; set; }
        }
        #endregion
        #region SelectedDrinkCommand
        public ICommand SelectedDrinkCommand { get; set; }
        private async void SelectedDrinkCommandExcute(Drink drink)
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("DrinkSelected", drink);
            await Navigation.NavigateAsync(PageManagement.ProductDetailPage, navParams);
        }
        #endregion
        public ICommand SelectedCategoryCommand { get; set; }
        private void SelectedCategoryExec()
        {
           
        }
    }
}
