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

        public readonly static string SignUpOptionPage = "SignUpOptionPage";
        public readonly static string SignUpDetailsPage = "SignUpDetailsPage";
        public readonly static string SendVerificationCodePage = "SendVerificationCodePage";
        public readonly static string VerificationOTPPage = "VerificationOTPPage";
        public readonly static string LoginPage = "LoginPage";
        public readonly static string MasterContainerPage = "MasterContainerPage";
        public readonly static string CreateAccountPage = "CreateAccountPage";
        public readonly static string PersonalInformationPage = "PersonalInformationPage";
        public readonly static string DriverLicenseldIdPage = "DriverLicenseldIdPage";
        public readonly static string ChooseYourRidePage = "ChooseYourRidePage";
        public readonly static string VehicleRegistrationPage = "VehicleRegistrationPage";
        public readonly static string PickupMapPage = "PickupMapPage";
        public readonly static string SignPickupPage = "SignPickupPage";
        public readonly static string ProfilePage = "ProfilePage";
        public readonly static string AccountPage = "AccountPage";
        public readonly static string EditDriverLicenseIdPage = "EditDriverLicenseIdPage";
        public readonly static string VehicleInformationPage = "VehicleInformationPage";
        public readonly static string ChangePasswordPage = "ChangePasswordPage";
        public readonly static string ContactUsPage = "ContactUsPage";
        public readonly static string TripDetailPage = "TripDetailPage";
        public readonly static string SettingPage = "SettingPage";
        public readonly static string TripHistoryPage = "TripHistoryPage";
        public readonly static string PaymentHistory = "PaymentHistory";
        
        


        #endregion
    }
}
