
using TheCoffeHouse.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace TheCoffeHouse.Views.Base
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetIconColor(this, Color.Black);                        
                    
        }

        #region Properties

        protected bool IsAppeared { get; private set; }
        protected BaseViewModel ViewModel;

        #endregion

        protected override void OnBindingContextChanged()
        {
            if (BindingContext != null)
                ViewModel = (BaseViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            try
            {
                if (ViewModel == null && BindingContext != null)
                    ViewModel = (BaseViewModel)BindingContext;

                if (!IsAppeared)
                    ViewModel?.OnFirstTimeAppear();

                IsAppeared = true;
                ViewModel?.OnAppear();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        protected override void OnDisappearing()
        {
            ViewModel?.OnDisappear();
        }

        #region BackButtonPress

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext == null)
            {
                return base.OnBackButtonPressed();
            }

            var bindingContext = BindingContext as BaseViewModel;

            if (bindingContext == null)
            {
                return base.OnBackButtonPressed();
            }

            var result = bindingContext?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = false;

        #endregion
    }
}
