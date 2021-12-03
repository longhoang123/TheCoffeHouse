using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        }

        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            Drink selectedDrink = parameters.GetValue<Drink>("DrinkSelected");
        }
    }
}
