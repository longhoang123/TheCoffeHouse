using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class LoginPageViewModel : BaseViewModel 
    {
        public LoginPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            LoginCommand = new DelegateCommand(LoginExecute);
        }

        
        #region Properties
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                SetProperty(ref _phoneNumber, value);
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        #endregion

        #region LoginCommand
        public ICommand LoginCommand { get; set; }
        private async void LoginExecute()
        {
            var user = await ApiService.ValidateUser(PhoneNumber, Password);
            if(user == null)
            {
                App.Current.MainPage.DisplayAlert("Lỗi", "Số điện thoại hoặc mật khẩu không đúng, vui lòng thử lại", "OK");
            }
            else
            {
                NavigationParameters navParam = new NavigationParameters();
                navParam.Add(ParamKey.CurrentUser.ToString(), user);

                await Navigation.GoBackAsync(navParam);
            }
        }


        #endregion
    }
}
