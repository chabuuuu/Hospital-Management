﻿using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Security.AccessControl;
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

        public bool deleteVisibility = true;
        public bool changeVisibility = true;
        public bool addVisibility = true;
        private List<YSI> nurse_doctor;
        private YSI selectedItem = null;

        public List<YSI> Nurse_doctor { get => nurse_doctor; set {
                nurse_doctor = value; 
                OnPropertyChanged(nameof(Nurse_doctor));
            } }
        public YSI SelectedItem { get => selectedItem; set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            } }
        public bool DeleteVisibility
        {
            get => deleteVisibility; set
            {
                deleteVisibility = value;
                OnPropertyChanged(nameof(DeleteVisibility));
            }
        }
        public bool AddVisibility
        {
            get => addVisibility; set
            {
                addVisibility = value;
                OnPropertyChanged(nameof(AddVisibility));
            }
        }
        public bool ChangeVisibility
        {
            get => changeVisibility; set
            {
                changeVisibility = value;
                OnPropertyChanged(nameof(ChangeVisibility));
            }
        }
        QUANLYBENHVIENEntities _db;
        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
            Nurse_doctor = _db.YSI.ToList();
        }

        public DoctorAndNurseViewModel()
        {
            Load();
            Set_permission(MainViewModel._currentUserAccount.LOAITAIKHOAN);
            // dựa vào class ViewModelCommand đã được định nghĩa
            DoctorCommand = new ViewModelCommand(ExecuteDoctorCommand, CanExecuteDoctorCommand);
            NurseCommand = new ViewModelCommand(ExecuteNurseCommand, CanExecuteNurseCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }
        void Set_permission(string type)
        {
            switch (type)
            {
                case "Admin":
                    Set_admin();
                    break;
                case "Staff":
                    Set_staff();
                    break;
                case "Doctor":
                    Set_doctor();
                    break;
                default:
                    MessageBox.Show("nothing");
                    break;
            }
        }
        void Set_doctor()
        {
            deleteVisibility = false;
            changeVisibility = false;
            addVisibility = false;
        }
        void Set_admin()
        {
            deleteVisibility = true;
            changeVisibility = true;
            addVisibility = true;
        }
        void Set_staff()
        {
            deleteVisibility = false;
            changeVisibility = false;
            addVisibility = false;
        }
        private bool CanExecuteDoctorCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteDoctorCommand(object? obj)
        {

        }

        private bool CanExecuteNurseCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteNurseCommand(object? obj)
        {

        }



        //tham số thứ 1 là điều kiện thực hiện command
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteAddCommand(object? obj)
        {
            AddNurseAndDoctor wd = new AddNurseAndDoctor();
            wd.Closed += AddNurseAndDoctor_Closed;
            //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
            // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void AddNurseAndDoctor_Closed(object sender, EventArgs e)
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
                int Id = SelectedItem.MAYSI;
                var deleteMember = _db.YSI.Where(m => m.MAYSI == Id).Single();
                _db.YSI.DeleteObject(deleteMember);
                _db.SaveChanges();
                new MessageBoxCustom("Thành công", "Đã xóa y sĩ: \nMã y sĩ: " +
                    SelectedItem.SUB_ID.ToString() + "\nHọ Tên: " +
                    SelectedItem.HOTEN.ToString(), 
                    MessageType.Success, MessageButtons.OK).ShowDialog();
                
                /*
                MessageBox.Show("Đã xóa y sĩ: \nMã y sĩ: " +
                    SelectedItem.SUB_ID.ToString() + "\nHọ Tên: " +
                    SelectedItem.HOTEN.ToString());
                */
                Load();
            }
            catch (Exception e)
            {
                //new MessageBoxCustom("Lỗi", e.Message + "\nLỗi: " + e.GetType().ToString() + e.InnerException.ToString(), MessageType.Error, MessageButtons.OKCancel).ShowDialog();
                new MessageBoxCustom("Lỗi", "Không thể xóa", MessageType.Error, MessageButtons.OKCancel).ShowDialog();
                //MessageBox.Show(e.Message + "\nLỗi: " + e.GetType().ToString() + e.InnerException.ToString());
            }
        }

        private bool CanExecuteViewCommand(object? obj)
        {
            return true;
        }
        //tham số thứ 2 là hành động
        private void ExecuteViewCommand(object? obj)
        {
            ViewDoctorAndNurse wd = new ViewDoctorAndNurse();
            if (SelectedItem != null)
            {
                wd.DataContext = new ViewNurseAndDoctorViewModel(SelectedItem);
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
            ChangeDoctorAndNurse wd = new ChangeDoctorAndNurse();
            wd.Closed += ChangeDoctorAndNurse_Closed;
            if (SelectedItem != null)
            {
                wd.DataContext = new ChangeNurseAndDoctorViewModel(SelectedItem);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }

        private void ChangeDoctorAndNurse_Closed(object sender, EventArgs e)
        {
            Load();
        }
    }
}
