using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Media;
using Prism;
using Prism.Ioc;

namespace TheCoffeHouse.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            CrossMedia.Current.Initialize();
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

