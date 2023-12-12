using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.ViewModel;

namespace LTTQ_DoAn.ViewModell {
    public class AddHeathRecordViewModel : BaseViewModel {
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public AddHeathRecordViewModel() {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }
        private bool CanExecuteCancelCommand(object? obj) {
            return true; //ko điều kiện
        }
        private void ExecuteCancelCommand(object? obj) {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteAddCommand(object? obj) {
            return true;
        }
        private void ExecuteAddCommand(object? obj) {
            //
            Application.Current.MainWindow.Close();
        }
 
    }
}
