using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
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
        public ICommand ChangeRoomCommand { get; }
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
