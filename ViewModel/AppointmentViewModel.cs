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
    public class AppointmentViewModel : BaseViewModel
    {
        public ICommand AddApointmentCommand { get; }
        public ICommand DeleteApointmentCommand { get; }

        public AppointmentViewModel()
        {
            // dựa vào class ViewModelCommand đã được định nghĩa
            AddApointmentCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteApointmentCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }

        //tham số thứ 1 là điều kiện thực hiện command
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteAddCommand(object? obj)
        {
            AddAppointment wd = new AddAppointment();
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        //tham số 1 điều kiện để xóa lịch khám
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDeleteCommand(object? obj)
        {

        }
    }
}
