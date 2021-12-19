using Prism.Commands;
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
    public class RewardPageViewModel : BaseViewModel
    {
        public RewardPageViewModel(
             INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "The Coffe House's Reward";

            OpenLoginPageCommand = new DelegateCommand(OpenLoginPageExecute);
        }

        


        #region OpenLoginPageCommand
        public ICommand OpenLoginPageCommand { get; set; }
        private async void OpenLoginPageExecute()
        {
            await Navigation.NavigateAsync(PageManagement.LoginPage);
        }
        #endregion
    }
}
