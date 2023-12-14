using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;

namespace LTTQ_DoAn.ViewModel
{
    public class AddRoomViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public AddRoomViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }

        private bool CanExecuteCancelCommand(object? obj)
        {
            return true; //ko dieu kien
        }

        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true; //dk: không trùng với cái đã có
        }

        private void ExecuteAddCommand(object? obj)
        {
            //
            Application.Current.MainWindow.Close();
        }
    }

}
