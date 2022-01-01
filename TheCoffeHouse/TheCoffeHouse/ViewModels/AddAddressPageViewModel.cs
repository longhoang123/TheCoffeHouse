using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TheCoffeHouse.Constant;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Models;
using TheCoffeHouse.Renderers.ToastMessage;
using TheCoffeHouse.Services;
using TheCoffeHouse.Services.ApiService;
using TheCoffeHouse.ViewModels.Base;
using Xamarin.Forms;

namespace TheCoffeHouse.ViewModels
{
    public class AddAddressPageViewModel : BaseViewModel
    {
        public AddAddressPageViewModel(INavigationService navigationService = null,
            IPageDialogService dialogService = null,
            IHttpService httpService = null,
            ISQLiteService sQLiteService = null) : base(navigationService, dialogService, httpService, sQLiteService)
        {
            PageTitle = "Thêm địa chỉ";
            CreateAddressCommand = new DelegateCommand(CreateAddressExecute);
        }

        #region Properties
        private int _type;

        public int Type
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private string _addressDetail;

        public string AddressDetail
        {
            get { return _addressDetail; }
            set
            {
                SetProperty(ref _addressDetail, value);
            }
        }
        
        private string _block;

        public string Block
        {
            get { return _block; }
            set
            {
                SetProperty(ref _block, value);
            }
        }
        
        private string _gate;

        public string Gate
        {
            get { return _gate; }
            set
            {
                SetProperty(ref _gate, value);
            }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set
            {
                SetProperty(ref  _note, value);
            }
        }

        private string _receiverName;

        public string ReceiverName
        {
            get { return _receiverName; }
            set
            {
                SetProperty(ref  _receiverName, value);
            }
        }

        private string _receiverPhone;

        public string ReceiverPhone
        {
            get { return _receiverPhone; }
            set
            {
                SetProperty(ref _receiverPhone, value);
            }
        }

        private int _isHomeType;

        public int IsHomeType
        {
            get { return _isHomeType; }
            set
            {
                SetProperty(ref _isHomeType, value);
            }
        }
        #endregion

        #region CreateAddressCommand
        public ICommand CreateAddressCommand { get; set; }
        private async void CreateAddressExecute()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(AddressDetail) || string.IsNullOrEmpty(ReceiverName) || string.IsNullOrEmpty(ReceiverPhone))
            {
                DependencyService.Get<Toast>().Show("Vui lòng nhập đầy đủ thông tin bắt buộc!");
                return;
            }
            Address address = new Address();
            address.UserID = int.Parse(ConstaintVaribles.UserID);
            address.Type = IsHomeType;
            address.Name = Name;
            address.AddressDetail = AddressDetail;
            address.Block = Block ?? "";
            address.Gate = Gate ?? "";
            address.Note = Note ?? "";
            address.ReceiverName = ReceiverName;
            address.ReceiverPhone = ReceiverPhone;

            var result = await ApiService.AddAddress(address);
            if(result != null)
            {
                DependencyService.Get<Toast>().Show("Tạo địa chỉ thành công");
                NavigationParameters navParams = new NavigationParameters();
                navParams.Add(ParamKey.NeedRefresh.ToString(), true);
                await Navigation.GoBackAsync(navParams);
            } else
            {
                DependencyService.Get<Toast>().Show("Có lỗi xảy ra, vui lòng thử lại");
            }
        }
        #endregion
    }
}
