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
            minusCommand = new DelegateCommand(minusExecute);
            plusCommand = new DelegateCommand(plusExecute);
            CheckedSizeNhoCmd = new DelegateCommand(CheckedSizeNhoExe);
            CheckedSizeVuaCmd = new DelegateCommand(CheckedSizeVuaExe);
            CheckedSizeLonCmd = new DelegateCommand(CheckedSizeLonExe);
        }
     
        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            selectedDrink = parameters.GetValue<Drink>("DrinkSelected");
            NamePro = selectedDrink.DrinkName;
            //ImagePro = selectedDrink.DrinkImage;
            PricePro = selectedDrink.DrinkPrice;
            DesPro = "Cà phê được pha phin truyền thống kết hợp với sữa đặc tạo nên hương vị đậm đà, hài hòa giữa vị ngọt đầu lưỡi và vị đắng thanh thoát nơi hậu vị.";
            QuantityDetailCart = 1;
            PriceTotal= selectedDrink.DrinkPrice;
            BonusPriceSize = 0;
        }

        #region Properties RadioButton
        private int _BonusPriceSize;

        public int BonusPriceSize
        {
            get { return _BonusPriceSize; }
            set {
                SetProperty(ref _BonusPriceSize, value);
                RaisePropertyChanged("BonusPriceSize");
            }
        }

        #endregion
        #region Properties

        private bool _AllowCommand;

        public bool AllowCommand
        {
            get { return _AllowCommand; }
            set
            {
                SetProperty(ref _AllowCommand, value);
                RaisePropertyChanged("AllowCommand");
            }
        }
        private int _PriceTotal;

        public int PriceTotal
        {
            get { return _PriceTotal; }
            set
            {
                SetProperty(ref _PriceTotal, value);
                RaisePropertyChanged("PriceTotal");
            }
        }

        private int _QuantityDetailCart;

        public int QuantityDetailCart
        {
            get { return _QuantityDetailCart; }
            set
            {
                SetProperty(ref _QuantityDetailCart, value);
                RaisePropertyChanged("QuantityDetailCart");
            }
        }


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

        private int _PricePro;

        public int PricePro
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
        #region minusCommand
        public ICommand minusCommand { get; set; }
        private void minusExecute()
        {
            if(QuantityDetailCart > 1)
            {
                QuantityDetailCart = QuantityDetailCart - 1;
                PriceTotal = selectedDrink.DrinkPrice * QuantityDetailCart;
            }
            else
            {
                AllowCommand = false;
            }
           
        }
        #endregion
        #region plusCommand
        public ICommand plusCommand { get; set; }
        private void plusExecute()
        {
            QuantityDetailCart = QuantityDetailCart + 1;
            PriceTotal = (selectedDrink.DrinkPrice) * QuantityDetailCart;
            AllowCommand = true;
        }
        #endregion
        #region CheckedSizeNhoCmd
        public ICommand CheckedSizeNhoCmd { get; set; }
        private void CheckedSizeNhoExe()
        {
            BonusPriceSize = 0;
            selectedDrink.DrinkPrice = PricePro + BonusPriceSize;
            PriceTotal = (selectedDrink.DrinkPrice) * QuantityDetailCart;

        }
        #endregion
        #region CheckedSizeVuaCmd
        public ICommand CheckedSizeVuaCmd { get; set; }
        private void CheckedSizeVuaExe()
        {
            BonusPriceSize = 6000;
            selectedDrink.DrinkPrice = PricePro + BonusPriceSize;
            PriceTotal = (selectedDrink.DrinkPrice) * QuantityDetailCart;
        }
        #endregion
        #region CheckedSizeLonCmd
        public ICommand CheckedSizeLonCmd { get; set; }
        private void CheckedSizeLonExe()
        {
            BonusPriceSize = 10000;
            selectedDrink.DrinkPrice = PricePro + BonusPriceSize;
            PriceTotal = (selectedDrink.DrinkPrice) * QuantityDetailCart;
        }
        #endregion
    }
}
