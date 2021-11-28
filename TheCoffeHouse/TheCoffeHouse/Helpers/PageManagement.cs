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

        public readonly static string MainPage = "MainPage";
        public readonly static string LoginPage = "LoginPage";
        public readonly static string TabContainerPage = "TabContainerPage";
        public readonly static string HomeTabPage = "HomeTabPage";
      
        
        


        #endregion
    }
}
