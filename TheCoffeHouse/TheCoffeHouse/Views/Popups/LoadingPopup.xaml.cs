using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Linq;
using TheCoffeHouse.Utilities;

namespace TheCoffeHouse.Views.Popups
{
    public partial class LoadingPopup : PopupPage
    {
        private static bool IsAppearing = false;

        public LoadingPopup()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            //IsLoading = false;
        }

        #region Instance

        private static LoadingPopup _instance;

        public static LoadingPopup Instance => _instance ?? (_instance = new LoadingPopup());

        #endregion

        #region Show

        public async Task Show()
        {
            if (IsAppearing) return;

            IsAppearing = true;

            await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
            {
                LoadingIndicator.IsRunning = true;
                await PopupNavigation.Instance.PushAsync(this, animate: true);
            });
        }

        #endregion

        #region Hide

        public async Task Hide()
        {
            if (IsAppearing)
            {
                // a little delay for appearance animation before hiding
                await Task.Delay(500);
                IsAppearing = false;

                if (PopupNavigation.Instance.PopupStack.Contains(this))
                {
                    await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                    {
                        LoadingIndicator.IsRunning = false;
                        await PopupNavigation.Instance.RemovePageAsync(this);
                    });
                }
            }
        }

        #endregion

        // Lock hard ware back button
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}