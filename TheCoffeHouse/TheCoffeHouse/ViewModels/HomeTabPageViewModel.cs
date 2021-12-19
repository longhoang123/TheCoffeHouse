using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;
using TheCoffeHouse.Views.Popups;
using Xamarin.Forms;

namespace TheCoffeHouse.ViewModels
{
    public class HomeTabPageViewModel : BaseViewModel
    {
        public HomeTabPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListBanner = new ObservableCollection<ImageSource>();
            ListPost = new ObservableCollection<HomePostItem>();
            ListPostUI = new ObservableCollection<HomePostUIItem>();

            OpenLoginPageCommand = new DelegateCommand(OpenLoginPageExecute);
            OpenDeliveryCommand = new DelegateCommand(OpenDeliveryExecute);
            OpenRewardPageCommand = new DelegateCommand(OpenRewardPageExecute);
            OpenEditBookingPopupCommand = new DelegateCommand(OpenEditBookingPopupExecute);
            OpenPost1Command = new DelegateCommand<HomePostUIItem>(selectedPostItem => OpenPost1Execute(selectedPostItem));
            OpenPost2Command = new DelegateCommand<HomePostUIItem>(selectedPostItem => OpenPost2Execute(selectedPostItem));

            Image = "BarCode.png";
        }

      





        #region Properties
        private ObservableCollection<ImageSource> _listBanner;

        public ObservableCollection<ImageSource> ListBanner
        {
            get { return _listBanner; }
            set 
            { 
                SetProperty(ref _listBanner, value);
                RaisePropertyChanged("ListBanner");
            }
        }

        private ObservableCollection<HomePostItem> _listPost;

        public ObservableCollection<HomePostItem> ListPost
        {
            get { return _listPost; }
            set
            {
                SetProperty(ref _listPost, value);
                RaisePropertyChanged("ListPost");
            }
        }

        private ObservableCollection<HomePostUIItem> _listPostUI;

        public ObservableCollection<HomePostUIItem> ListPostUI
        {
            get { return _listPostUI; }
            set
            {
                SetProperty(ref _listPostUI, value);
                RaisePropertyChanged("ListPostUI");
            }
        }

        private string _image;

        public string Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
            }
        }

        private string _bottomTitle = "Giao tận nơi";

        public string BottomTitle
        {
            get { return _bottomTitle; }
            set
            {
                SetProperty(ref _bottomTitle, value);
            }
        }

        private string _bottomAddress = "Chọn địa chỉ của bạn";

        public string BottomAddress
        {
            get { return _bottomAddress; }
            set
            {
                SetProperty(ref _bottomAddress, value);
            }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                SetProperty(ref _user, value);
            }
        }

        private Store _selectedStore;

        public Store SelectedStore
        {
            get { return _selectedStore; }
            set
            {
                SetProperty(ref _selectedStore, value);
            }
        }

        #endregion

        public override async void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            LoadData();
        }

        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);

            var user = new User();

            if (parameters != null && parameters.Keys.Count() > 0)
            {
                if (parameters.TryGetValue(ParamKey.CurrentUser.ToString(), out user))
                {
                    User = user;
                    IsLogedin = ConstaintVaribles.IsLogedIn;
                }
                SelectedStore = parameters.GetValue<Store>(ParamKey.StoreSelected.ToString()) ?? new Store{ StoreAddress = "Chọn cửa hàng để đến lấy"};
                BottomTitle = "Đến lấy tại";
                BottomAddress = SelectedStore.StoreAddress;
            }
        }

        private async void LoadData()
        {
            ListPost = await ApiService.GetPosts() ?? new ObservableCollection<HomePostItem>();
            SelectedStore = new Store { StoreAddress = "Chọn cửa hàng để đến lấy", StoreDistance = "" };

            foreach (var post in ListPost)
            {
                ListBanner.Add(post.Image);
            }
           
            for(int i = 0; i < ListPost.Count + 1; i += 2)
            {
                if(i + 1 >= ListPost.Count)
                {
                    var item = (i + 1) == ListPost.Count ? ListPost[i] : null;
                    if(item == null)
                    {
                        return;
                    }
                    ListPostUI.Add(new HomePostUIItem
                    {
                        Url1 = item.Url,
                        Title1 = item.Title,
                        Image1 = item.Image,
                        Date1 = item.Date,
                        IsEvenNumber = false,

                    }) ;
                    return;
                }
                var item1 = ListPost[i];
                var item2 = ListPost[i + 1] ?? new HomePostItem();
                ListPostUI.Add(new HomePostUIItem
                {
                    Title1 = item1.Title,
                    Image1 = item1.Image,
                    Date1 = item1.Date,
                    Url1 = item1.Url,
                    Title2 = item2.Title,
                    Date2 = item2.Date,
                    Image2 = item2.Image,
                    Url2 = item2.Url,
                    
                });
            }



        }


        #region OpenLoginPageCommand
        public ICommand OpenLoginPageCommand { get; set; }
        private  void OpenLoginPageExecute()
        {
            Navigation.NavigateAsync(PageManagement.LoginPage);
        }
        #endregion

        #region OpenDeliveryCommand
        public ICommand OpenDeliveryCommand { get; set; }
        private async void OpenDeliveryExecute()
        {
            await Navigation.SelectTabAsync(PageManagement.OrderPage);
        }
        #endregion

        #region OpenRewardPageCommand
        public ICommand OpenRewardPageCommand { get; set; }
        private async void OpenRewardPageExecute()
        {
            await Navigation.NavigateAsync(PageManagement.RewardPage);
        }
        #endregion


        #region OpenPost1Command
        public ICommand OpenPost1Command { get; set; }
        private async void OpenPost1Execute(HomePostUIItem selectedPostItem)
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add(ParamKey.SelectedPost.ToString(), selectedPostItem.Url1);

            await Navigation.NavigateAsync(PageManagement.PreferentialPage, navParam);
        }
        #endregion

        #region OpenPost2Command
        public ICommand OpenPost2Command { get; set; }
        private async void OpenPost2Execute(HomePostUIItem selectedPostItem)
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add(ParamKey.SelectedPost.ToString(), selectedPostItem.Url2);

            await Navigation.NavigateAsync(PageManagement.PreferentialPage, navParam);
        }
        #endregion

        #region OpenEditBookingPopupCommand
        public ICommand OpenEditBookingPopupCommand { get; set; }
        private async void OpenEditBookingPopupExecute()
        {
            await EditBookingPopup.Instance.Show("Đại học công nghệ thông tin", "Thạch Hoàng Long", SelectedStore.StoreAddress, SelectedStore.StoreDistance, () => OpenStoreFunc(), () => OpenAddressFunc());
        }

        #region OpenStoreFunc

        public Task OpenStoreFunc()
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add(ParamKey.IsNavigateFromMainPage.ToString(), true);
            return Navigation.NavigateAsync(PageManagement.StorePage, navParam);
        }
        #endregion

        #region OpenAddressFunc

        public Task OpenAddressFunc()
        {
            return Navigation.NavigateAsync(PageManagement.AddressPage);
        }
        #endregion

        #endregion
    }
}
