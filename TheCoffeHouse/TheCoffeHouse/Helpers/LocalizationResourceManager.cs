using TheCoffeHouse.Resources;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using Xamarin.Essentials;

namespace TheCoffeHouse.Helpers
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        private const string LanguageKey = nameof(LanguageKey);

        public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        private LocalizationResourceManager()
        {
            SetCulture(new CultureInfo(Preferences.Get(LanguageKey, CurrentCulture.TwoLetterISOLanguageName)));
        }

        public string this[string text] 
        {
            get
            {
                return AppResources.ResourceManager.GetString(text, AppResources.Culture);
            }
        }
        public void SetCulture(CultureInfo language)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            AppResources.Culture = language;
            Preferences.Set(LanguageKey, language.TwoLetterISOLanguageName);

            Invalidate();
        }
        public string GetValue(string text)
        {
            ResourceManager resourceManager = new ResourceManager(typeof(AppResources).ToString(), typeof(TranslateExtension).GetTypeInfo().Assembly);
            return resourceManager.GetString(text, AppResources.Culture);
        }
        public CultureInfo CurrentCulture => AppResources.Culture ?? Thread.CurrentThread.CurrentUICulture;

        public event PropertyChangedEventHandler PropertyChanged;
        public void Invalidate()
        {          
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
