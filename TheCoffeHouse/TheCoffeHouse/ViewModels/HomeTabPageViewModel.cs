using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;
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

        #endregion

        public override async void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            LoadData();
        }

        private async void LoadData()
        {
            for(var i = 0; i < 7; i++ )
            {
                ListBanner.Add("home_banner.png");
                ListPost.Add(new HomePostItem { Title = $"Pick up món ngon, đón Tết an toàn {i}", Image = "Post1.png", Date = new DateTime(2021, 02, 03)});
            }
           
            for(int i = 0; i < ListPost.Count + 1; i += 2)
            {
                if(i + 1 >= ListPost.Count)
                {
                    var item = ListPost[i];
                    ListPostUI.Add(new HomePostUIItem
                    {
                        Title1 = item.Title,
                        Image1 = item.Image,
                        Date1 = item.Date,
                        IsEvenNumber = false,
                        
                    });
                    return;
                }
                var item1 = ListPost[i];
                var item2 = ListPost[i + 1] ?? new HomePostItem();
                ListPostUI.Add(new HomePostUIItem
                {
                    Title1 = item1.Title,
                    Image1 = item1.Image,
                    Date1 = item1.Date,
                    Title2 = item2.Title,
                    Date2 = item2.Date,
                    Image2 = item2.Image
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
    }
}
