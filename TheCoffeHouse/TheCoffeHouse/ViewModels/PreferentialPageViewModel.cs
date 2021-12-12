using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class PreferentialPageViewModel : BaseViewModel
    {
        public PreferentialPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Ưu đãi đặc biệt";
        }

        public override void OnNavigatedNewTo(INavigationParameters parameters)
        {
            base.OnNavigatedNewTo(parameters);
            var url = "";
            if (parameters != null && parameters.Keys.Count() > 0)
            {
                if (parameters.TryGetValue(ParamKey.SelectedPost.ToString(), out url))
                {
                    PromoUrl = url;
                }
            }
        }

        #region Properties
        private string _promoUrl;

        public string PromoUrl
        {
            get { return _promoUrl; }
            set
            {
                SetProperty(ref _promoUrl, value);
            }
        }
        #endregion
    }
}
