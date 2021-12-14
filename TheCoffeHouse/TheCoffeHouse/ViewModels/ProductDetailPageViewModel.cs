using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class ProductDetailPageViewModel : BaseViewModel
    {
        public ProductDetailPageViewModel(
             INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            OpenCartPageCommand = new DelegateCommand(OpenCartPageExecute);
        }
     
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            selectedDrink = parameters.GetValue<Drink>("DrinkSelected");
            NamePro = selectedDrink.DrinkName;
            ImagePro = selectedDrink.DrinkImage;
            PricePro = selectedDrink.DrinkPrice.ToString();
            DesPro = "Cà phê được pha phin truyền thống kết hợp với sữa đặc tạo nên hương vị đậm đà, hài hòa giữa vị ngọt đầu lưỡi và vị đắng thanh thoát nơi hậu vị.";
        }

        #region Properties

        private string _NamePro;

        public string NamePro
        {
            get { return _NamePro; }
            set
            {
                SetProperty(ref _NamePro, value);
                RaisePropertyChanged("NamePro");
            }
        }

        private string _PricePro;

        public string PricePro
        {
            get { return _PricePro; }
            set
            {
                SetProperty(ref _PricePro, value);
                RaisePropertyChanged("PricePro");
            }
        }
        private string _DesPro;

        public string DesPro
        {
            get { return _DesPro; }
            set
            {
                SetProperty(ref _DesPro, value);
                RaisePropertyChanged("DesPro");
            }
        }
        private Drink _selectedDrink;
        public Drink selectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                SetProperty(ref _selectedDrink, value);
                RaisePropertyChanged("selectedDrink");
            }
        }
        private string _ImagePro;

        public string ImagePro
        {
            get { return _ImagePro; }
            set
            {
                    SetProperty(ref _ImagePro, value);
                    RaisePropertyChanged(ImagePro);
            }
        }

        #endregion
        #region OpenCartPageCommand
        public ICommand OpenCartPageCommand { get; set; }
        private void OpenCartPageExecute()
        {
            Navigation.NavigateAsync(PageManagement.CartPage);
        }
        #endregion
    }
}
