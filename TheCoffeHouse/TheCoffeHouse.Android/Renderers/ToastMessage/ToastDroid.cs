using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoffeHouse.Droid.Renderers.ToastMessage;
using TheCoffeHouse.Renderers.ToastMessage;

[assembly: Xamarin.Forms.Dependency(typeof(ToastDroid))]

namespace TheCoffeHouse.Droid.Renderers.ToastMessage
{
    public class ToastDroid : TheCoffeHouse.Renderers.ToastMessage.Toast
    {
       

        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}