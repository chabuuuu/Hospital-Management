using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;

namespace LTTQ_DoAn.ViewModel {
    public class ViewFieldViewModel {
        public ICommand CancelCommand { get; }
        public ViewFieldViewModel() {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }

        private bool CanExecuteCancelCommand(object? obj) {
            return true;//ko điều kiện
        }

        private void ExecuteCancelCommand(object? obj) {
            Application.Current.MainWindow.Close();
        }
    }
}
