﻿using LTTQ_DoAn.Model;
using LTTQ_DoAn.View;
using LTTQ_DoAn.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using LTTQ_DoAn.ViewModell;
using System.Data.Entity.Infrastructure;
using System.Runtime.InteropServices.ComTypes;

namespace LTTQ_DoAn.ViewModel
{
    public class BenhAnType
    {
        public int MABENHAN { get; set; }
        public string SUB_ID { get; set; }
        public string NGAYKHAM { get; set; }
        public string KHOA { get; set; }
        public string TRIEUCHUNG { get; set; }
        public string KETLUAN { get; set; }
        public string MAYSI { get; set; }
        public string THANHTIEN { get; set; }
        public string DICHVU { get; set; }
    }
    public class DonThuocType
    {
        public int MADONTHUOC { get; set; }
        public string SUB_IDDT { get; set; }
        public string GHICHU { get; set; }
        public string MAYSI { get; set; }
        public string? NGAYLAPDONTHUOC { get; set; }
        public string KHOA { get; set; }
        public string BACSI { get; set; }
        public string BENHAN { get; set; }

    }
    public class HealthRecordAndPrescriptionViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        public bool changeVisibility = true;
        public bool addVisibility = true;
        private TAIKHOAN _currentUserAccount;
        private string khoa;
        private HealthRecordAndPrescription thisView;
        public ICommand ChangeHRCommand { get; }
        public ICommand AddHRCommand { get; }
        public ICommand DeleteHRCommand { get; }
        public ICommand ChangePCommand { get; }
        public ICommand AddPCommand { get; }
        public ICommand DeletePCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand ViewHistory { get; }
        public BENHNHAN Benhnhan
        {
            get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        public List<BenhAnType> Listbenhan { get => listbenhan; set
            {
                listbenhan = value;
                OnPropertyChanged(nameof(Listbenhan));
            }
        }

        public string Khoa
        {
            get => khoa; set
            {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
            }
        }

        public BenhAnType Benhan
        {
            get => benhan; set
            {
                benhan = value;
                OnPropertyChanged(nameof(Benhan));
            }
        }
        public List<DonThuocType> Listdonthuoc
        {
            get => listdonthuoc; set
            {
                listdonthuoc = value;
                OnPropertyChanged(nameof(Listdonthuoc));
            }
        }
        public DonThuocType Donthuoc
        {
            get => donthuoc; set
            {
                donthuoc = value;
                OnPropertyChanged(nameof(Donthuoc));
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
        public TAIKHOAN CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        private BENHNHAN benhnhan;
        private List<DonThuocType> listdonthuoc;
        private DonThuocType donthuoc;
        private List<BenhAnType> listbenhan;
        private BenhAnType benhan;
        private void findBenhAn()
        {
            _db = new QUANLYBENHVIENEntities();
            List<BENHAN> query = (from m in _db.BENHAN
                                  where m.MABENHNHAN == Benhnhan.MABENHNHAN
                                  select m).ToList();
            List<BenhAnType> list = new List<BenhAnType>();
            foreach (var item in query)
            {
                BenhAnType benhan = new BenhAnType() {
                MABENHAN = item.MABENHAN,
                SUB_ID = item.SUB_ID,
                NGAYKHAM = item.NGAYKHAM.ToString(),
                KHOA = findKhoa(item.MAYSI),
                TRIEUCHUNG = item.TRIEUCHUNG,
                KETLUAN = item.KETLUAN,
                MAYSI = findBacSi(item.MAYSI),
                DICHVU = findDichVu(item.MADICHVU),
                //THANHTIEN = item.THANHTIEN.ToString()
                THANHTIEN = (item.DICHVU.GIATIEN + item.DONTHUOC
                           .Sum(k => k.CHITIETDONTHUOC
                           .Sum(l => l.THUOC.GIATIEN * (((decimal)((int)(l.SOLUONG * 10000))) / 10000))
                           )).ToString(),
            };
                list.Add(benhan);
            }
            Listbenhan = list;
        }
        private string findBacSi(int? maBacSi)
        {
            if (maBacSi == null)
            {
                return "";
            }
            string? tenBacSi = (from m in _db.YSI
                                where m.MAYSI == maBacSi
                                select m.HOTEN).First();
            if (tenBacSi == null)
            {
                return "";
            }
            return "M" + maBacSi.ToString() + ": " + tenBacSi;
        }
        private string findDichVu(int? maDichVu)
        {
            if (maDichVu == null)
            {
                return "";
            }
            string? tenDichVu = (from m in _db.DICHVU
                                 where m.MADICHVU == maDichVu
                                 select m.TENDICHVU).First();
            if (tenDichVu == null)
            {
                return "";
            }
            return tenDichVu;
        }
        private string findKhoa(int? maYsi)
        {
            if (maYsi == null)
            {
                return "";
            }
            int? maKhoa = (from m in _db.YSI
                       where m.MAYSI == maYsi
                       select m.MAKHOA).First();
            if (maKhoa == null)
            {
                return "";
            }
            string? tenKhoa = (from m in _db.KHOA
                               where m.MAKHOA == maKhoa
                               select m.TENKHOA).First();
            if (tenKhoa == null)
            {
                return "";
            }
            return "K" + maKhoa.ToString() + ": " + tenKhoa;
        }
        private void findDonThuoc()
        {
            _db = new QUANLYBENHVIENEntities();
            List<DONTHUOC> query = (from m in _db.BENHAN
                                    join n in _db.DONTHUOC on m.MABENHAN equals n.MABENHAN
                                    where m.MABENHNHAN == Benhnhan.MABENHNHAN
                                    select n).ToList();

            List<DonThuocType> list = new List<DonThuocType>();
            foreach (var item in query)
            {
                DonThuocType donthuoc = new DonThuocType()
                {
                    MADONTHUOC = item.MADONTHUOC,
                    SUB_IDDT = item.SUB_ID,
                    GHICHU = item.GHICHU,
                    MAYSI = findYSi(item.MABENHAN),
                    BACSI = item.BENHAN.YSI.HOTEN,
                    NGAYLAPDONTHUOC = item.BENHAN.NGAYKHAM.ToString(),
                    KHOA = item.BENHAN.YSI.KHOA.TENKHOA,
                    BENHAN = item.BENHAN.SUB_ID,
                };
                list.Add(donthuoc);
            }
            Listdonthuoc = list;
        }
        private string findYSi(int? maBenhAn)
        {
            if (maBenhAn == null)
            {
                return "";
            }
            int? maYSi = (from m in _db.BENHAN
                          where m.MABENHAN == maBenhAn
                          select m.MAYSI).First();
            if (maYSi == null)
            {
                return "";
            }
            string? tenYSi = (from m in _db.YSI
                              where m.MAYSI == maYSi
                              select m.HOTEN).First();
            if (tenYSi == null)
            {
                return "";
            }
            return "M" + maYSi.ToString() + ": " + tenYSi;
        }
        public HealthRecordAndPrescriptionViewModel()
        {
            AddHRCommand = new ViewModelCommand(ExecuteAddHRCommand, CanExecuteAddHRCommand);
            DeleteHRCommand = new ViewModelCommand(ExecuteDeleteHRCommand, CanExecuteDeleteHRCommand);
            ChangeHRCommand = new ViewModelCommand(ExecuteChangeHRCommand, CanExecuteChangeHRCommand);
            AddPCommand = new ViewModelCommand(ExecuteAddPCommand, CanExecuteAddPCommand);
            DeletePCommand = new ViewModelCommand(ExecuteDeletePCommand, CanExecuteDeletePCommand);
            ChangePCommand = new ViewModelCommand(ExecuteChangePCommand, CanExecuteChangePCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
            ViewHistory = new ViewModelCommand(ExecuteViewHistoryCommand, CanExecuteViewHistoryCommand);
        }
        public HealthRecordAndPrescriptionViewModel(BENHNHAN SelectedBenhNhan, HealthRecordAndPrescription view, TAIKHOAN user_account)
        {
            this.thisView = view;
            Benhnhan = SelectedBenhNhan;
            findBenhAn();
            findDonThuoc();
            CurrentUserAccount = user_account;
            Set_permission(CurrentUserAccount.LOAITAIKHOAN);
            AddHRCommand = new ViewModelCommand(ExecuteAddHRCommand, CanExecuteAddHRCommand);
            DeleteHRCommand = new ViewModelCommand(ExecuteDeleteHRCommand, CanExecuteDeleteHRCommand);
            ChangeHRCommand = new ViewModelCommand(ExecuteChangeHRCommand, CanExecuteChangeHRCommand);
            AddPCommand = new ViewModelCommand(ExecuteAddPCommand, CanExecuteAddPCommand);
            DeletePCommand = new ViewModelCommand(ExecuteDeletePCommand, CanExecuteDeletePCommand);
            ChangePCommand = new ViewModelCommand(ExecuteChangePCommand, CanExecuteChangePCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
            ViewHistory = new ViewModelCommand(ExecuteViewHistoryCommand, CanExecuteViewHistoryCommand);
        }

        private bool CanExecuteViewHistoryCommand(object obj)
        {
            return true;
        }

        private void ExecuteViewHistoryCommand(object obj)
        {
            ViewHistoryPrescrip wd = new ViewHistoryPrescrip();
            wd.Closed += ViewHistory_Closed;
            if (Benhan != null)
            {
                wd.DataContext = new HealthRecordHistoryViewModel(Benhan.MABENHAN);
                System.Windows.Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }

        private void ViewHistory_Closed(object sender, EventArgs e)
        {
            findBenhAn();
        }

        void Set_permission(string type)
        {
            switch (type)
            {
                case "Admin":
                    Set_admin();
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
            changeVisibility = false;
            addVisibility = true;
        }
        void Set_admin()
        {
            changeVisibility = true;
            addVisibility = false;
        }
        private bool CanExecuteExitCommand(object obj)
        {
            return true;
        }

        private void ExecuteExitCommand(object obj)
        {
            //System.Windows.Application.Current.MainWindow = ;

            //System.Windows.Application.Current.MainWindow.Close();
            thisView.Close();
        }

        private bool CanExecuteAddHRCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddHRCommand(object? obj)
        {
            AddHealthRecord wd = new AddHealthRecord();
            wd.Closed += AddHealthRecord_Closed;
            wd.DataContext = new AddHeathRecordViewModel(Benhnhan);
            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();

        }

        private void AddHealthRecord_Closed(object sender, EventArgs e)
        {
            findBenhAn();
        }

        private bool CanExecuteDeleteHRCommand(object? obj)
        {
            return true;
        }
        
        private void ExecuteDeleteHRCommand(object? obj)
        {
            try
            {
                int Id = Benhan.MABENHAN;
                var deleteMember = _db.BENHAN.Where(m => m.MABENHAN == Id).Single();
                _db.BENHAN.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa bệnh án: \nMã bệnh án: " +
                        Benhan.SUB_ID.ToString() + "\nNgày khám: " +
                        Benhan.NGAYKHAM.ToString(),
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                findBenhAn();
            }
            catch (Exception e)
            {
                new MessageBoxCustom(
                    "Thông báo",
                    "Vui lòng xóa đơn thuốc thuộc bệnh án trước",
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }
        private bool CanExecuteChangeHRCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeHRCommand(object? obj)
        {
            if (Benhan == null)
            {
                return;
            }
                ChangeHealthRecord wd = new ChangeHealthRecord();
                wd.Closed += ChangeHealthRecord_Closed;
                wd.DataContext = new ChangeHealthRecordViewModel(Benhan.MABENHAN);
                System.Windows.Application.Current.MainWindow = wd;
                wd.ShowDialog();
        }

        private void ChangeHealthRecord_Closed(object sender, EventArgs e)
        {
            findBenhAn();
        }
        private bool CanExecuteAddPCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddPCommand(object? obj)
        {
            AddPrescription wd = new AddPrescription();
            wd.Closed += AddPrescription_Closed;
            wd.DataContext = new AddPrescriptionViewModel(Benhnhan);
            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();

        }
        private void AddPrescription_Closed(object sender, EventArgs e)
        {
            findDonThuoc();
        }

        private bool CanExecuteDeletePCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeletePCommand(object? obj)
        {
            try
            {
                int Id = Donthuoc.MADONTHUOC;
                var deleteMember = _db.DONTHUOC.Where(m => m.MADONTHUOC == Id).Single();
                _db.DONTHUOC.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa đơn thuốc: " + Donthuoc.SUB_IDDT.ToString(),
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                findDonThuoc();
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

        private bool CanExecuteChangePCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangePCommand(object? obj)
        {
            //System.Windows.MessageBox.Show("hello");
            if (Donthuoc == null)
            {
                return;
            }
            ChangePrescription wd = new ChangePrescription();
            wd.Closed += ChangePrescription_Closed;
            wd.DataContext = new ChangePrescriptionViewModel(Donthuoc.MADONTHUOC);
            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void ChangePrescription_Closed(object sender, EventArgs e)
        {
            findDonThuoc();
        }


    }
}
