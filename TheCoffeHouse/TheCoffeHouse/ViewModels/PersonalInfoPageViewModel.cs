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
using TheCoffeHouse.Constant;
using TheCoffeHouse.Models;
using TheCoffeHouse.Renderers.ToastMessage;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;
using TheCoffeHouse.Views.Popups;
using Xamarin.Forms;

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
        

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
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


        private ImageSource _avatar;

        public ImageSource Avatar
        {
            get { return _avatar; }
            set
            {
                SetProperty(ref _avatar, value);
            }
        }

        private string _avatarBase64;

        public string AvatarBase64
        {
            get { return _avatarBase64; }
            set
            {
                SetProperty(ref _avatarBase64, value);
            }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                SetProperty(ref _birthDate, value);
            }
        }
        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
            }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                SetProperty(ref _user, value);
            }
        }

        #endregion
        #region Command
        public ICommand UpdateInfoCommand { get; set; }
        private async void UpdateInfoCommandExcute()
        {
            var user = new User();
            user.UserID = int.Parse(ConstaintVaribles.UserID);
            user.Name = Name;
            user.Phone = PhoneNumber;
            user.Email = Email;
            user.Avatar = AvatarBase64 ?? User.Avatar;
            user.Birthdate = BirthDate;
            user.Gender = Gender;
            var result = await ApiService.UpdateUser(user);
            if(result != null)
            {
                DependencyService.Get<Toast>().Show("Update succesfully");
                await Navigation.GoBackAsync();
            }
        }
        public ICommand ChangeAvatarCommand { get; set; }
        private async Task ChangeAvatarCommandExcute()
        {
            await CheckNotBusy(async () =>
            {
                var mediafile = await PickPhoto();
                Avatar = ImageSource.FromStream(mediafile.GetStream);
                AvatarBase64 = await ConvertToBase64(mediafile);
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

        public override async void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            await LoadingPopup.Instance.Show();
            await FetchData();
            await LoadingPopup.Instance.Hide();
        }

        private async Task FetchData()
        {
            User = await ApiService.GetUserByID(int.Parse(ConstaintVaribles.UserID)) ?? new User();
            Avatar = ConvertFromBase64(User.Avatar);
            Name = User.Name;
            PhoneNumber = User.Phone;
            Email = User.Email ?? "";
            BirthDate = User.Birthdate == null ? DateTime.Now : (DateTime)User.Birthdate;
            Gender = User.Gender == null ? 0 : (int)User.Gender;
        }
    }
}
