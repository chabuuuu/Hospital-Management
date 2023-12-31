using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class RoomViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db;

        private List<PHONG> phong;
        private PHONG selectedphong;
        private List<BENHNHAN> benhnhan;
        public ICommand ChangeRoomCommand { get; }
        public ICommand ViewInfoCommand { get; }
        public ICommand AddRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }
        public List<PHONG> Phong
        {
            get => phong; set
            {
                phong = value;
                OnPropertyChanged(nameof(Phong));
            }
        }
        public PHONG Selectedphong
        {
            get => selectedphong; set
            {
                selectedphong = value;
                OnPropertyChanged(nameof(Selectedphong));
            }
        }

        public List<BENHNHAN> Benhnhan
        {
            get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        private void LoadBenhNhan()
        {
            Benhnhan =  Selectedphong.BENHNHAN.ToList();
        }

        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
            Phong = _db.PHONG.ToList();
        }

        public RoomViewModel()
        {
            Load();
            ChangeRoomCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddRoomCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteRoomCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewInfoCommand = new ViewModelCommand(ExecuteViewInfoCommand, CanExecuteViewInfoCommand);
        }

        private bool CanExecuteViewInfoCommand(object? obj)
        {
            return true;
        }
        private void ExecuteViewInfoCommand(object? obj)
        {
            new MessageBoxCustom("hello", "chao ban", MessageType.Success, MessageButtons.OKCancel).ShowDialog();
            LoadBenhNhan();
        }

        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeCommand(object? obj)
        {
            ChangeService wd = new ChangeService();

            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddService wd = new AddService();

            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj)
        {

        }

    }
}
