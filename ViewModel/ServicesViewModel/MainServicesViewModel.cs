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
    public class ServicesViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db;
        public bool deleteVisibility = true;
        public bool changeVisibility = true;
        public bool addVisibility = true;
        private List<DICHVU> dichvu;
        private DICHVU selecteddichvu;
        public ICommand ChangeServicesCommand { get; }
        public ICommand AddServicesCommand { get; }
        public ICommand DeleteServicesCommand { get; }
        public List<DICHVU> Dichvu
        {
            get => dichvu; set
            {
                dichvu = value;
                OnPropertyChanged(nameof(Dichvu));
            }
        }
        public DICHVU Selecteddichvu
        {
            get => selecteddichvu; set
            {
                selecteddichvu = value;
                OnPropertyChanged(nameof(Selecteddichvu));
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
            Dichvu = _db.DICHVU.ToList();
        }
        public ServicesViewModel()
        {
            Load();
            Set_permission(MainViewModel._currentUserAccount.LOAITAIKHOAN);
            ChangeServicesCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddServicesCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteServicesCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
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
        private bool CanExecuteChangeCommand(object? obj) {
            return true;
        }
        private void ExecuteChangeCommand(object? obj) {
            ChangeService wd = new ChangeService();
            wd.Closed += ChangeService_Closed;

            if (Selecteddichvu != null)
            {
                wd.DataContext = new ChangeServicesViewModel(Selecteddichvu);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }

        }

        private void ChangeService_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteAddCommand(object? obj) {
            return true;
        }
        private void ExecuteAddCommand(object? obj) {
            AddService wd = new AddService();
            wd.Closed += AddService_Closed;
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void AddService_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteDeleteCommand(object? obj) {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj) {
            try
            {
                int Id = Selecteddichvu.MADICHVU;
                var deleteMember = _db.DICHVU.Where(m => m.MADICHVU == Id).Single();
                _db.DICHVU.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa dịch vụ: \nMã dịch vụ: " +
                        Selecteddichvu.SUB_ID.ToString() + "\nTên dịch vụ: " +
                        Selecteddichvu.TENDICHVU,
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Load();
            }
            catch (Exception e)
            {
                new MessageBoxCustom(
                    "Thông báo",
                    "Vui lòng xóa các bệnh án, lịch khám\n có sử dụng dịch vụ",
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }

    }
}
