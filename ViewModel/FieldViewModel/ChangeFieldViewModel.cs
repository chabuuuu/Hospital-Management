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
    public class ChangeFieldViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public ChangeFieldViewModel() {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);

        }

        private bool CanExecuteCancelCommand(object? obj) {
            return true; //ko điều kiện
        }
        private void ExecuteCancelCommand(object? obj) {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteChangeCommand(object? obj) {
            return true; //dk: thay đổi xong không trùng với khoa đã có
        }
        private void ExecuteChangeCommand(object? obj) {
            //
            Application.Current.MainWindow.Close();
        }
    }
}
