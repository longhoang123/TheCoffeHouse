using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;
using TheCoffeHouse.Views.Base;

namespace TheCoffeHouse.ViewModels
{
    public class SettingPageViewModel : BaseViewModel
    {
        public SettingPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {

        }
    }
}
