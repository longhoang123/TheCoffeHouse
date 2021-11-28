using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheCoffeHouse.Services;
using TheCoffeHouse.ViewModels.Base;

namespace TheCoffeHouse.ViewModels
{
    public class HistoryPageViewModel : BaseViewModel
    {
        public HistoryPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            ListHistory = new ObservableCollection<DetailHistoryOrder>();
            init();
        }
        #region ClassDefine
        public class DetailHistoryOrder
        {
            public int IDHistoryOrder { get; set; }
            public string DetailHistoryOrderInfo { get; set; }
        }
        #endregion

        #region ListHistoryInit
        //Thêm danh sách lịch sử order từ db
        private void init()
        {
            ListHistory.Add(new DetailHistoryOrder { IDHistoryOrder = 1, DetailHistoryOrderInfo = "Đơn hàng 1" });
        }
        #endregion
        #region Properties
        private ObservableCollection<DetailHistoryOrder> _listHistory;
        public ObservableCollection<DetailHistoryOrder> ListHistory
        {
            get => _listHistory;
            set
            {
                SetProperty(ref _listHistory, value);
                RaisePropertyChanged("HelloWord");
            }
        }
        #endregion
    }
}
