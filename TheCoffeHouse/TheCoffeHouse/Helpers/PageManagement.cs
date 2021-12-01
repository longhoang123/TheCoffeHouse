using TheCoffeHouse.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace TheCoffeHouse.Helpers
{
    public class PageManagement
    {
        #region MultiplePages

        public static string MultiplePages(string[] pages)
        {
            var path = "";
            if (pages == null) return "";
            if (pages.Length < 1) return "";
            for (var i = 0; i < pages.Length; i++)
            {
                path += i == 0 ? pages[i] : "/" + pages[i];
            }
            return path;
        }

        #endregion

        #region GetCurrentPage

        public static Page GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;
            var navStack = mainPage.Navigation.NavigationStack;

            if (navStack == null)
                return mainPage;

            if (navStack.Count < 1)
                return mainPage;

            return navStack.Last();
        }

        public static Page GetCurrentPage(bool withModal)
        {

            if (!withModal) return GetCurrentPage();
            try
            {
                var navPage = GetCurrentPage();
                var modalPage = navPage.Navigation.ModalStack.LastOrDefault();
                var foundedPage = modalPage ?? navPage;
                return foundedPage;
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif
                return null;
            }
        }

        public static BaseViewModel GetCurrentPageBaseViewModel()
        {
            return (BaseViewModel)GetCurrentPage(true).BindingContext;
        }

        #endregion

        #region GoBack

        public static string GoBack(string page = "", int number = 1)
        {
            var home = "../";

            var mainPage = Application.Current.MainPage;

            var navStack = mainPage.Navigation.NavigationStack;
            if (number < 1 || number >= navStack.Count)
                return "";


            for (; number < navStack.Count; number++)
            {
                home += "../";
            }

            return $"{home}{page}";
        }

        #endregion

        #region Pages

        public readonly static string LoginPage = "LoginPage";
        public readonly static string TabContainerPage = "TabContainerPage";
        public readonly static string HomeTabPage = "HomeTabPage";
        public readonly static string OtherPage = "OtherPage";
        public readonly static string HistoryPage = "HistoryPage";
        public readonly static string PolicyPage = "PolicyPage";
        public readonly static string VotePage = "VotePage";
        public readonly static string ContactPage = "ContactPage";
        public readonly static string PersonalInfoPage = "PersonalInfoPage";
        public readonly static string AddressPage = "AddressPage";
        public readonly static string SettingPage = "SettingPage";
        public readonly static string CollectPointPage = "CollectPointPage";
        public readonly static string ExchangePromotionPage = "ExchangePromotionPage";
        public readonly static string AllCouponPage = "AllCouponPage";
        public readonly static string DetailCouponPage = "DetailCouponPage";
        public readonly static string AllPromotionPage = "AllPromotionPage";
        public readonly static string DetailPromotionPage = "DetailPromotionPage";
        public readonly static string OrderPage = "OrderPage";
        public readonly static string StorePage = "StorePage";

        
        #endregion
    }
}
