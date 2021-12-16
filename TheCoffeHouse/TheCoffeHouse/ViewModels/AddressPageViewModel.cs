﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class AddressPageViewModel : BaseViewModel
    {
        public AddressPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            OpenAddAddressPage = new DelegateCommand(OpenAddAddressPageExcute);
        }
        public ICommand OpenAddAddressPage { get; set; }
        private async void OpenAddAddressPageExcute()
        {
            await Navigation.NavigateAsync(PageManagement.AddAddressPage);
        }
    }
}
