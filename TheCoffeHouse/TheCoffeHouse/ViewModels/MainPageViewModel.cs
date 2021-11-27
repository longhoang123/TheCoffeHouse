using Prism.Commands;
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
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Main Page";
            ListString = new ObservableCollection<string>();   
            OpenPage1Command = new DelegateCommand(OpenPage1Execute);
            OpenOrderPageCommand = new DelegateCommand(OpenOrderPageExecute);
            OpenStorePageCommand = new DelegateCommand(OpenStorePageExecute);

        }

        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            InitData();

        }

        private void InitData()
        {
            HelloWord = "Hello xamarin form";
            for(var i = 0; i < 20; i++)
            {
                ListString.Add("abc");
            }
        }

        public override void OnNavigatedBackTo(INavigationParameters parameters)
        {
            base.OnNavigatedBackTo(parameters);
        }

        #region Properties
        public string abc { get; set; }

        private string _helloWord;

        public string HelloWord
        {
            get => _helloWord;
            set => SetProperty(ref _helloWord, value);
        }

        private ObservableCollection<string> _listString;

        public ObservableCollection<string> ListString
        {
            get => _listString;
            set
            {
                SetProperty(ref _listString, value);
                RaisePropertyChanged("HelloWord");
            }
        }

        #endregion

        #region OpenPage1Command
        public ICommand OpenPage1Command { get; set; }
        private async void OpenPage1Execute()
        {
            string abc = "abc";
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add(ParamKey.Username.ToString(), abc);
            await Navigation.NavigateAsync(PageManagement.Page1, navParams);
        }
        #endregion
        #region OpenOrderPageCommand
        public ICommand OpenOrderPageCommand { get; set; }
        private async void OpenOrderPageExecute()
        {
            //NavigationParameters navParams = new NavigationParameters();
            //navParams.Add()
            await Navigation.NavigateAsync(PageManagement.OrderPage);
        }
        #endregion
        #region OpenOrderPageCommand
        public ICommand OpenStorePageCommand { get; set; }
        private async void OpenStorePageExecute()
        {
            //NavigationParameters navParams = new NavigationParameters();
            //navParams.Add()
            await Navigation.NavigateAsync(PageManagement.StorePage);
        }
        #endregion

    }
}
