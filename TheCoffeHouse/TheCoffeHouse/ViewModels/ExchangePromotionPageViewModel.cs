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
            PageTitle = "Đổi ưu đãi";
            ListPromotion = new ObservableCollection<Promotion>();
            ListSpecialPromotion = new ObservableCollection<Promotion>();
            init();
            OpenAllPromotionPage = new DelegateCommand(OpenAllPromotionPageExcute);
            SelectedSpecialPromotionCommand = new DelegateCommand<Promotion>(SelectedPromotion => SelectedSpecialPromotionCommandExcute(SelectedPromotion));
        }
        private async void init()
        {
            ListPromotion = await ApiService.GetPromotions() ?? new ObservableCollection<Promotion>();
            int max = 0;
            int submax = -1;
            for(int i=0;i<ListPromotion.Count;i++)
            {
                if(Convert.ToInt32(ListPromotion[i].Point)>=max)
                {
                    submax = max;
                    max = Convert.ToInt32(ListPromotion[i].Point);
                }
            }
            for (int i = 0; i < ListPromotion.Count; i++)
            {
                if(Convert.ToInt32(ListPromotion[i].Point)==max || Convert.ToInt32(ListPromotion[i].Point)==submax)
                {
                    ListSpecialPromotion.Add(ListPromotion[i]);
                }
                if(ListSpecialPromotion.Count==2)
                {
                    break;
                }
            }
        }
        #region OpenPage
        
        private async void OpenDetailPromotionPageExcute()
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("PromotionSelected", SelectedPromotion);
            await Navigation.NavigateAsync(PageManagement.DetailPromotionPage, navParams);
        }

        public ICommand SelectedSpecialPromotionCommand { get; set; }
        private async void SelectedSpecialPromotionCommandExcute(Promotion promotion)
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("PromotionSelected", promotion);
            await Navigation.NavigateAsync(PageManagement.DetailPromotionPage, navParams);
        }

        public ICommand OpenAllPromotionPage { get; set; }
        private async void OpenAllPromotionPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AllPromotionPage);
        }
        #endregion


        #region ListPromotion
        private ObservableCollection<Promotion> _listPromotion;
        public ObservableCollection<Promotion> ListPromotion
        {
            get => _listPromotion;
            set
            {
                SetProperty(ref _listPromotion, value);
                RaisePropertyChanged("ListPromotion");
            }
        }

        private ObservableCollection<Promotion> _listSpecialPromotion;
        public ObservableCollection<Promotion> ListSpecialPromotion
        {
            get => _listSpecialPromotion;
            set
            {
                SetProperty(ref _listSpecialPromotion, value);
                RaisePropertyChanged("ListSpecialPromotion");
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
