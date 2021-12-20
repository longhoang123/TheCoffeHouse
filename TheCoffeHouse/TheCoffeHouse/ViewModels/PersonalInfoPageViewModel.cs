using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TheCoffeHouse.Models;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class PersonalInfoPageViewModel : BaseViewModel
    {
        public PersonalInfoPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            UpdateInfoCommand = new DelegateCommand(UpdateInfoCommandExcute);
            ChangeAvatarCommand = new DelegateCommand(async () => await ChangeAvatarCommandExcute());
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

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
            }
        }


        private string _avatar;

        public string Avatar
        {
            get { return _avatar; }
            set
            {
                SetProperty(ref _avatar, value);
            }
        }

        #endregion
        #region Command
        public ICommand UpdateInfoCommand { get; set; }
        private async void UpdateInfoCommandExcute()
        {
            User user = new User();
            user.Name = FirstName + LastName;
            user.Phone = PhoneNumber;
            var avatar = Avatar;
        }
        public ICommand ChangeAvatarCommand { get; set; }
        private async Task ChangeAvatarCommandExcute()
        {
            await CheckNotBusy(async () =>
            {
                var mediafile = await PickPhoto();
                Avatar = await ConvertToBase64(mediafile);
            });
        }

        async Task<MediaFile> PickPhoto()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                CompressionQuality = 80,
                PhotoSize = PhotoSize.Medium,
                SaveMetaData = true,
            });

            return file;
        }

        #endregion
    }
}
