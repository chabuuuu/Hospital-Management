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
    public class DoctorAndNurseViewModel : BaseViewModel
    {
        public ICommand DoctorCommand { get; } // lệnh chuyển sang xem bác sĩ
        public ICommand NurseCommand { get; } // lệnh chuyển sang xem y tá

        public ICommand ViewCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public DoctorAndNurseViewModel() {
            // dựa vào class ViewModelCommand đã được định nghĩa
            DoctorCommand = new ViewModelCommand(ExecuteDoctorCommand, CanExecuteDoctorCommand);
            NurseCommand = new ViewModelCommand(ExecuteNurseCommand, CanExecuteNurseCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }

        private bool CanExecuteDoctorCommand(object? obj) {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDoctorCommand(object? obj) {

        }

        private bool CanExecuteNurseCommand(object? obj) {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteNurseCommand(object? obj) {

        }



        //tham số thứ 1 là điều kiện thực hiện command
        private bool CanExecuteAddCommand(object? obj) {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteAddCommand(object? obj) {
            AddNurseAndDoctor wd = new AddNurseAndDoctor();
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        //tham số 1 điều kiện để xóa lịch khám
        private bool CanExecuteDeleteCommand(object? obj) {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDeleteCommand(object? obj) {

        }

        private bool CanExecuteViewCommand(object? obj) {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteViewCommand(object? obj) {
            ViewDoctorAndNurse wd = new ViewDoctorAndNurse();
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteChangeCommand(object? obj) {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteChangeCommand(object? obj) {
            ChangeDoctorAndNurse wd = new ChangeDoctorAndNurse();
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
    }
}
