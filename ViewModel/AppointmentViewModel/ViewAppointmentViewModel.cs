using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel {
    public class ViewAppointmentViewModel : BaseViewModel {
        public ICommand CancelCommand { get; }
        public ViewAppointmentViewModel() {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        private void ExecuteCancelCommand(object? obj) {
            Application.Current.MainWindow.Close();
        }
        //điều kiện để lệnh hủy bỏ được thực hiện: k có điều kiện
        private bool CanExecuteCancelCommand(object? obj) {
            return true;
        }
    }
}
