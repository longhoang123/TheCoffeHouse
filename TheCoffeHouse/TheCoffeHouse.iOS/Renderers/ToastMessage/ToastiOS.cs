using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoffeHouse.iOS.Renderers.ToastMessage;
using TheCoffeHouse.Renderers.ToastMessage;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(ToastiOS))]

namespace TheCoffeHouse.iOS.Renderers.ToastMessage
{
    public class ToastiOS : Toast
    {
        const double LONG_DELAY = 3.5;


        NSTimer alertDelay;
        UIAlertController alert;

        public void Show(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }


        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
 }