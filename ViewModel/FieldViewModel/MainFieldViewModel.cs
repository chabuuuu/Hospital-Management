﻿using LTTQ_DoAn.View;
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
    public class FieldViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db;
        public bool deleteVisibility = true;
        public bool changeVisibility = true;
        public bool addVisibility = true;
        private List<KHOA> khoa;
        private KHOA selectedkhoa;
        public ICommand ViewFieldCommand { get; }
        public ICommand ChangeFieldCommand { get; }
        public ICommand AddFieldCommand { get; }
        public ICommand DeleteFieldCommand { get; }
        public List<KHOA> Khoa
        {
            get => khoa; set
            {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
            }
        }

        public KHOA Selectedkhoa
        {
            get => selectedkhoa; set
            {
                selectedkhoa = value;
                OnPropertyChanged(nameof(selectedkhoa));
            }
        }
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
        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
            Khoa = _db.KHOA.ToList();
        }
        public FieldViewModel()
        {
            Load();
            Set_permission(MainViewModel._currentUserAccount.LOAITAIKHOAN);
            ViewFieldCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeFieldCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddFieldCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteFieldCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
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
        private bool CanExecuteViewCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void ExecuteViewCommand(object? obj)
        {
            ViewField wd = new ViewField();
            if (Selectedkhoa != null)
            {
                wd.DataContext = new ViewFieldViewModel(Selectedkhoa);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }


        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void ExecuteChangeCommand(object? obj)
        {

            ChangeField wd = new ChangeField();
            wd.Closed += ChangeField_Closed;
            if (Selectedkhoa != null)
            {
                wd.DataContext = new ChangeFieldViewModel(Selectedkhoa);
                //cài mainwindow thành cửa số mới mở này để chút nữa đóng lại thì ta chỉ cần dùng lệnh close cho mainwindow
                // vi dụ nút cancel ở trong AddAppointmentViewModel.cs
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }

        }

        private void ChangeField_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true;//ko điều kiện
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddField wd = new AddField();
            wd.Closed += AddField_Closed;
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void AddField_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;//ko điều kiện
        }
        private void ExecuteDeleteCommand(object? obj)
        {
            try
            {
                int Id = Selectedkhoa.MAKHOA;
                var deleteMember = _db.KHOA.Where(m => m.MAKHOA == Id).Single();
                _db.KHOA.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa khoa: \nMã khoa: " +
                        Selectedkhoa.SUB_ID.ToString() + "\nTên khoa: " +
                        Selectedkhoa.TENKHOA,
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Load();
            }
            catch (Exception e)
            {
                new MessageBoxCustom(
                    "Thông báo",
                    "Vui lòng xóa tất cả các bác sĩ thuộc khoa trước",
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }

    }
}
