using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class SignupPageViewModel : BaseViewModel 
    {
        public SignupPageViewModel(
             INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            SignupCommand = new DelegateCommand(SignupExecute);
        }

        #region Properties
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }
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
        private string _cfPassword;

        public string CfPassword
        {
            get { return _cfPassword; }
            set
            {
                SetProperty(ref _cfPassword, value);
            }
        }
        #endregion

        #region SignupCommand
        public ICommand SignupCommand { get; set; }
        private async void SignupExecute()
        {
            if(await ValidateInputAsync())
            {
                await Navigation.GoBackAsync();
                
            }
        }

        private async Task<bool> ValidateInputAsync()
        {
            
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(CfPassword))
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Vui lòng điền đầy đủ thông tin", "OK");
            }
            else if (Password != CfPassword)
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Mật khẩu không trùng khớp", "OK");
            }
            else
            {
                var check = 0;
                var user = await ApiService.CheckExisted(PhoneNumber);
                if (user==null)
                {
                    var result = await ApiService.RegisterUser(new User { Phone = PhoneNumber, Name = Name, Password = Password });
                    check = 1;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", "Số điện thoại đã tồn tại", "OK");
                }
                return check == 1 ? true : false;
                
            }
            return false;
                

        }


        #endregion
    }
}
