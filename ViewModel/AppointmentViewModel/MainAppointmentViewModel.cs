using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class LichKhamType
    {
        public LICHKHAM Lichkham { get; set; }
        public BENHNHAN Benhnhan { get; set; }
        public DICHVU Dichvu { get; set; }
    }
    public class AppointmentViewModel : BaseViewModel
    {
        public ICommand ViewAppointmentCommand { get; }
        public ICommand ChangeAppointmentCommand { get; }
        public ICommand AddApointmentCommand { get; }
        public ICommand DeleteApointmentCommand { get; }

        private List<LichKhamType> lichkhams;
        private LichKhamType selectedItem = null;


        QUANLYBENHVIENEntities _db;
        public List<LichKhamType> LICHKHAMs
        {
            get => lichkhams; set
            {
                lichkhams = value;
                OnPropertyChanged(nameof(LICHKHAMs));
            }
        }
        
        public LichKhamType SelectedItem
        {
            get => selectedItem; set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        
        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
            var join_query =

            (from dv in _db.DICHVU
             from bn in _db.BENHNHAN
             join lk in _db.LICHKHAM on bn.MABENHNHAN equals lk.MABENHNHAN
             where dv.MADICHVU == lk.MADICHVU
             select new
             {
                 Lichkham = lk,
                 Benhnhan = bn,
                 Dichvu = dv
             }).ToList();
            /*
            var query =
               (from lichkham in _db.LICHKHAM
               join benhnhan in _db.BENHNHAN on lichkham.MABENHNHAN equals benhnhan.MABENHNHAN
               select new { Lichkham = lichkham, Benhnhan = benhnhan }).ToList();
            */
            /*
            List<object> list = new List<object>();
            foreach (var item in join_query)
            {
                list.Add(item);
            }
            LICHKHAMs = list;
            */

            List<LichKhamType> list = new List<LichKhamType>();
            foreach (var item in join_query)
            {
                LichKhamType newLichKham = new LichKhamType() { 
                    Lichkham = item.Lichkham,
                    Benhnhan = item.Benhnhan,
                    Dichvu = item.Dichvu,
                };
                list.Add(newLichKham);
            }
            LICHKHAMs = list;
        }

        public AppointmentViewModel()
        {
            Load();
            // dựa vào class ViewModelCommand đã được định nghĩa
            AddApointmentCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteApointmentCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewAppointmentCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeAppointmentCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
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
            wd.Closed += AddAppointment_Closed;
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void AddAppointment_Closed(object sender, EventArgs e)
        {
            Load();
        }

        //tham số 1 điều kiện để xóa lịch khám
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDeleteCommand(object? obj)
        {
            try
            {
                int Id = SelectedItem.Lichkham.MALICHKHAM;
                var deleteMember = _db.LICHKHAM.Where(m => m.MALICHKHAM == Id).Single();
                _db.LICHKHAM.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa lịch khám: \nMã lịch khám: " +
                        SelectedItem.Lichkham.SUB_ID.ToString() + "\nBệnh nhân: " +
                        SelectedItem.Benhnhan.HOTEN.ToString() +
                    "\nNgày được hẹn khám: " + 
                        SelectedItem.Lichkham.NGAYKHAM.ToString(),  
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Load();
            }
            catch (Exception e)
            {
                new MessageBoxCustom(
                    "Thông báo",
                    e.Message + "\nLỗi: " + e.GetType().ToString(),
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }

        private bool CanExecuteViewCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteViewCommand(object? obj)
        {
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            ViewAppointment wd = new ViewAppointment();
            if (SelectedItem != null)
            {
                wd.DataContext = new ViewAppointmentViewModel(SelectedItem.Lichkham, SelectedItem.Dichvu, SelectedItem.Benhnhan);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }

        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteChangeCommand(object? obj)
        {
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            
            ChangeAppointment wd = new ChangeAppointment();
            wd.Closed += ChangeAppointment_Closed;
            if (SelectedItem != null)
            {
               wd.DataContext = new ChangeAppointmentViewModel(SelectedItem.Lichkham);
                //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
                // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }

        private void ChangeAppointment_Closed(object sender, EventArgs e)
        {
            Load();
        }
    }
}
