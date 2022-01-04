using Prism.Commands;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Models;
using TheCoffeHouse.Renderers.ToastMessage;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheCoffeHouse.Views.Popups
{
    public partial class UpdateMailPopup : PopupPage
    {
        public UpdateMailPopup()
        {
            InitializeComponent();
        }

        #region Properties

        private static UpdateMailPopup _instance;

        public static bool IsAppearing { get; private set; }

        protected TaskCompletionSource<bool> Proccess;



        public static UpdateMailPopup Instance => _instance ?? (_instance = new UpdateMailPopup());

        #endregion

        #region Show

        public async Task<bool> Show()
        {
            if (IsAppearing)
                return false;

            IsAppearing = true;

            Proccess = new TaskCompletionSource<bool>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PushAsync(this);
            });

            var result = await GetResult();

            await Hide();
            IsAppearing = false;

            return result;
        }
        #endregion
        #region Hide

        public async Task Hide()
        {
            if (PopupNavigation.Instance.PopupStack.Count != 0)
                await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                {
                    await PopupNavigation.Instance.PopAsync(true);
                });
        }

        #endregion

        #region GetResult

        public Task<bool> GetResult()
        {
            if (Proccess != null)
            {
                return Proccess.Task;
            }

            return null;
        }

        #endregion     

        #region OnBackgroundTapped

        private void OnBackgroundTapped(object sender, EventArgs e)
        {
            
            Proccess.SetResult(false);
        }

        #endregion


        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = await ApiService.GetUserByID(int.Parse(ConstaintVaribles.UserID)) ?? new User();
            user.Email = EmailEntry.Text;
            if(user.Gender==null)
            {
                user.Gender = 0;
            }
            if(user.Birthdate == null)
            {
                user.Birthdate = DateTime.Now;
            }
            var result = await ApiService.UpdateUser(user);
            if (result != null)
            {
                ConstaintVaribles.user = user;
                DependencyService.Get<Toast>().Show("Thêm email thành công!");
                await Hide();
            }
            
        }
    }
}