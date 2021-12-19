using Plugin.Connectivity;
using Plugin.SecureStorage;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using TheCoffeHouse.Enums;  
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using TheCoffeHouse.Constant;
using NavigationMode = Prism.Navigation.NavigationMode;
using Prism.Navigation.TabbedPages;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using System.IO;

namespace TheCoffeHouse.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        #region Properties

        public static BaseViewModel Instance;

        private string _pageTitle;

        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }
        private bool _isLogedin = ConstaintVaribles.IsLogedIn;

        public bool IsLogedin
        {
            get => _isLogedin;
            set => SetProperty(ref _isLogedin, value);
        }

        public INavigationService Navigation { get; private set; }
        public IPageDialogService DialogService { get; private set; }
        public IHttpService HttpService { get; set; }
        public ISQLiteService SQLiteService { get; }

        #endregion

        #region Constructors

        public BaseViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null)
        {
            if (navigationService != null) Navigation = navigationService;
            if (dialogService != null) DialogService = dialogService;
            if (httpService != null) HttpService = httpService;
            if (sQLiteService != null) SQLiteService = sQLiteService;
            BackCommand = new DelegateCommand(async () => await BackExecute());
            OpenNotificationCommand = new DelegateCommand(async () => await OpenNotificationExecute());
            OpenCollectionPointCommand = new DelegateCommand(async () => await OpenCollectionPointExecute());
            OpenCartPageCommand = new DelegateCommand(async () => await OpenCartPageExec());
            Instance = this;
            
        }

        #endregion

        #region Navigate

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigated from");
#endif
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigating to");
#endif
        }

        public async virtual void OnNavigatedTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigated to");
#endif
            if (parameters != null)
            {
                try
                {
                    var navMode = parameters.GetNavigationMode();
                    switch (navMode)
                    {
                        case NavigationMode.New: OnNavigatedNewTo(parameters); break;
                        case NavigationMode.Back: OnNavigatedBackTo(parameters); break;
                    }
                }
                catch (Exception)
                {
                    OnNavigatedNewTo(parameters);
                }
                
            }

        }

        public virtual void OnNavigatedNewTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigate new to");
#endif
        }

        public virtual void OnNavigatedBackTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigate back to");
#endif
        }

        #endregion

        #region OnAppear / Disappear

        public virtual void OnAppear()
        {
        }

        public virtual void OnFirstTimeAppear()
        {
            
        }

        public virtual void OnDisappear()
        {

        }

        #endregion

        #region GetMessageError

        public string GetMessageError(HttpsError error)
        {
            switch (error)
            {
                case HttpsError.Null:
                    return "We cannot connect to server due to several reasons. Do you want to try again?";
                case HttpsError.InternetConnection:
                    return "Connection lost. Please check on the internet connection and try again.";
                case HttpsError.RequestCancellation:
                    return "We cannot connect to server due to several reasons. Do you want to try again?";
                case HttpsError.RequestTimeout:
                    return "The system is busy at the moment. Please try again or come back later.";
                default:
                    return "We cannot connect to server due to several reasons. Do you want to try again?";
            }

        }

        #endregion

        #region BackCommand

        public ICommand BackCommand { get; }

        protected virtual async Task BackExecute()
        {
            await CheckNotBusy(async () =>
            {
                await Navigation.GoBackAsync(null, null, true);
            });
        }

        #endregion

        #region BackButtonPress

        /// <summary>
        /// //false is default value when system call back press
        /// </summary>
        /// <returns></returns>
        public virtual bool OnBackButtonPressed()
        {
            BackExecute();

            return true;

        }

        /// <summary>
        /// called when page need override soft back button
        /// </summary>
        public virtual void OnSoftBackButtonPressed() { }

        #endregion

        #region CheckBusy
        /// <summary>
        /// Check app is busy or not
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        protected async Task CheckNotBusy(Func<Task> function)
        {
            if (App.IsNotBusy)
            {
                App.IsNotBusy = false;
                try
                {
                    await function();
                }
                catch (Exception e)
                {
#if DEBUG
                    Debug.WriteLine(e);
#endif
                }
                finally
                {
                    App.IsNotBusy = true;
                }
            }
        }

        #endregion

        #region CheckInternetConnection

        public bool IsInternetConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        #endregion

        #region OpenNotificationCommand
        public ICommand OpenNotificationCommand { get; set; }
        protected virtual async Task OpenNotificationExecute() {
            await Navigation.NavigateAsync(PageManagement.NotificationPage);
        }
        #endregion

        #region OpenCollectionPointCommand
        public ICommand OpenCollectionPointCommand { get; set; }
        protected virtual async Task OpenCollectionPointExecute()
        {
            await Navigation.SelectTabAsync(PageManagement.CollectPointPage);
            await Navigation.NavigateAsync(PageManagement.AllCouponPage);

        }
        #endregion

        #region OpenCartPageCommand
        public ICommand OpenCartPageCommand { get; set; }
        protected virtual async Task OpenCartPageExec()
        {
            await Navigation.NavigateAsync(PageManagement.CartPage);
        }
        #endregion

        #region ConvertToBase64
        public async Task<string> ConvertToBase64(MediaFile photo)
        {
            var stream = photo.GetStream();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);          
        }
        #endregion

        #region ConvertToBase64
        public ImageSource ConvertFromBase64(string base64Image)
        {
            byte[] Base64Stream = Convert.FromBase64String(base64Image);
            return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
        }
    }
        #endregion
}
