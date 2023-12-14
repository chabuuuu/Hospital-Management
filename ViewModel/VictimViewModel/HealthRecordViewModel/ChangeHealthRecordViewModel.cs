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
    public class ChangeHealthRecordViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public ChangeHealthRecordViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteConfirmChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteConfirmChangeCommand(object? obj)
        {
            //
            Application.Current.MainWindow.Close();
        }
    }
}
