using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCoffeHouse.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheCoffeHouse.Views.Popups
{
    public partial class DetailPromotionPopup : PopupPage
    {
        public DetailPromotionPopup()
        {
            InitializeComponent();
        }

        #region Properties

        private static DetailPromotionPopup _instance;

        public static bool IsAppearing { get; private set; }

        protected TaskCompletionSource<bool> Proccess;



        public static DetailPromotionPopup Instance => _instance ?? (_instance = new DetailPromotionPopup());

        #endregion

        #region Show

        public async Task<bool> Show(string brand, string code, string discount, string description)
        {
            if (IsAppearing)
                return false;

            IsAppearing = true;
            brandLabel.Text ="Thương hiệu: " + brand;
            codeLabel.Text = "Mã giảm giá: " + code;
            discountLabel.Text ="Giảm giá "+ discount + "%";
            descriptionLabel.Text = description;

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

    }
}