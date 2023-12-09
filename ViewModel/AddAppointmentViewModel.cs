using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//thêm 3 thư viện mới này
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;

namespace LTTQ_DoAn.ViewModel
{
    public class AddAppointmentViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddApointmentCommand { get; }
        public AddAppointmentViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddApointmentCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }
        //hành động của nút hủy bỏ: đóng cửa sổ
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        //điều kiện để lệnh hủy bỏ được thực hiện: k có điều kiện
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }

        //hành động thêm vào
        private void ExecuteAddCommand(object? obj)
        {
            //câu lệnh thêm ở đây
            
            Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
        }
        //điều kiện để lệnh thêm được thực hiện: lich khám không có sẵn trong database
        private bool CanExecuteAddCommand(object? obj)
        {
            // những điều kiện cần xét

            // nếu không thỏa sẽ show messagebox rằng đã có trong lịch khám và return false
            return true; // khi nào thỏa điều kiện sẽ chấp nhận
        }
    }

}
