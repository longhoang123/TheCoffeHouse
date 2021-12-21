using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class AdminMasterPageViewModel : BaseViewModel
    {
        public AdminMasterPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Admin Master Page";
            OpenManageAccountPage = new DelegateCommand(OpenManageAccountPageExcute);
            OpenManageCatePage = new DelegateCommand(OpenManageCatePageExcute);
            OpenManageDrinkPage = new DelegateCommand(OpenManageDrinkPageExcute);
            OpenManageOrderPage = new DelegateCommand(OpenManageOrderPageExcute);
            OpenManagePromotionPage = new DelegateCommand(OpenManagePromotionPageExcute);
            OpenManagePromoPostPage = new DelegateCommand(OpenManagePromoPostPageExcute);
            OpenManageCouponPage = new DelegateCommand(OpenManageCouponPageExcute);
        }
        #region OpenPage
        public ICommand OpenManageAccountPage { get; set; }
        private async void OpenManageAccountPageExcute()
        {
            //Quản lý tài khoản
        }
        public ICommand OpenManageCatePage { get; set; }
        private async void OpenManageCatePageExcute()
        {
            //Quản lý danh mục
        }
        public ICommand OpenManageDrinkPage { get; set; }
        private async void OpenManageDrinkPageExcute()
        {
            //Quản lý sản phẩm
        }
        public ICommand OpenManageOrderPage { get; set; }
        private async void OpenManageOrderPageExcute()
        {
            //Quản lý hóa đơn
        }
        public ICommand OpenManagePromotionPage { get; set; }
        private async void OpenManagePromotionPageExcute()
        {
            //Quản lý khuyến mãi
        }
        public ICommand OpenManagePromoPostPage { get; set; }
        private async void OpenManagePromoPostPageExcute()
        {
            //Quản lý bài khuyến mãi
        }
        public ICommand OpenManageCouponPage { get; set; }
        private async void OpenManageCouponPageExcute()
        {
            //Quản lý coupon
        }
        #endregion

    }
}