﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.View;
using MaterialDesignThemes.Wpf;

namespace LTTQ_DoAn.ViewModel
{
    public class BenhAnBackupType
    {
        public int MABENHAN { get; set; }
        public int MABENHNHAN { get; set; }
        public int MADICHVU { get; set; }
        public DateTime NGAYKHAM { get; set; }
        public string TRIEUCHUNG { get; set; }
        public string KETLUAN { get; set; }
        public int MAYSI { get; set; }
        public decimal THANHTIEN { get; set; }
    }
    public class ChangeHealthRecordViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private BENHAN benhan;
        private string dichvu;
        private string chiphi;
        private string bacsi;
        private BenhAnBackupType benhan_history;
        private List<String> dichvulist;
        private List<String> bacsilist;
        public List<String> DichVuList
        {
            get => dichvulist; set
            {
                dichvulist = value;
                OnPropertyChanged(nameof(DichVuList));
            }
        }

        public List<string> Bacsilist
        {
            get => bacsilist; set
            {
                bacsilist = value;
                OnPropertyChanged(nameof(Bacsilist));
            }
        }
        public string Bacsi
        {
            get => bacsi; set
            {
                bacsi = value;
                OnPropertyChanged(nameof(Bacsi));
            }
        }
        public string Chiphi
        {
            get => chiphi; set
            {
                chiphi = value;
                OnPropertyChanged(nameof(Chiphi));
            }
        }
        public string Dichvu
        {
            get => dichvu; set
            {
                dichvu = value;
                OnPropertyChanged(nameof(Dichvu));
                UpdateChiphi(dichvu);
            }
        }
        protected void UpdateChiphi(string Dichvu)
        {
            Dichvu = Dichvu.Substring(5);
            List<DICHVU> dichvu = _db.DICHVU.ToList();
            for (int i = 0; i < dichvu.Count; i++)
                if (dichvu[i].TENDICHVU == Dichvu)
                    Chiphi = dichvu[i].GIATIEN.ToString();
        }
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public BENHAN Benhan { get => benhan; set
            {
                benhan = value;
                OnPropertyChanged(nameof(Benhan));
            }
        }

        public BenhAnBackupType Benhan_history
        {
            get => benhan_history; set
            {
                benhan_history = value;
            }
        }

        public void loadDichvu()
        {
            List<DICHVU> dichvu = _db.DICHVU.ToList();
            List<String> subID = new List<String>();
            foreach (var item in dichvu)
            {
                subID.Add(item.SUB_ID + ": " + item.TENDICHVU);
            }
            this.DichVuList = subID;
        }
        public void loadBacsi()
        {
            List<YSI> bacsi = _db.YSI.ToList();
            List<String> subID = new List<String>();
            foreach (var item in bacsi)
            {
                if (item.LOAIYSI == null)
                {
                    continue;
                }
                if (item.LOAIYSI.Substring(0, 6).Equals("Bác sĩ"))
                {
                    subID.Add(item.HOTEN + ": " + item.SUB_ID);
                }
            }
            this.Bacsilist = subID;

        }

        public ChangeHealthRecordViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private void findBenhAn(int maBenhAn)
        {

            BENHAN benhAn = (from m in _db.BENHAN
                                  where m.MABENHAN == maBenhAn
                                  select m).First();
            Benhan = benhAn;
        }
        public int convertDichvuSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(2);
            return int.Parse(k1);
        }
        public int convertBacsiSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[1].Substring(2);
            return int.Parse(k1);
        }
        private void update()
        {
            BENHAN updateBenhan = (from m in _db.BENHAN
                                       where m.MABENHAN == Benhan.MABENHAN
                                       select m).Single();
            updateBenhan.MAYSI = convertBacsiSub_ID(Bacsi);
            updateBenhan.MABENHNHAN = Benhan.BENHNHAN.MABENHNHAN;
            updateBenhan.MADICHVU = convertDichvuSub_ID(Dichvu);
            updateBenhan.THANHTIEN = Decimal.Parse(Chiphi);
            updateBenhan.TRIEUCHUNG = Benhan.TRIEUCHUNG;
            updateBenhan.KETLUAN = Benhan.KETLUAN;
            _db.SaveChanges();
        }
        private void createHistory()
        {
            BENHAN_HISTORY newBenhAn_History = new BENHAN_HISTORY()
            {
                MABENHAN = Benhan_history.MABENHAN,
                MAYSI = Benhan_history.MAYSI,
                MADICHVU = Benhan_history.MADICHVU,
                NGAYKHAM = Benhan_history.NGAYKHAM,
                MABENHNHAN = Benhan_history.MABENHNHAN,
                THANHTIEN = Benhan_history.THANHTIEN,
                TRIEUCHUNG = Benhan_history.TRIEUCHUNG,
                KETLUAN = Benhan_history.KETLUAN,
                CHANGED_DATE = DateTime.Now,
            };
            _db.BENHAN_HISTORY.AddObject(newBenhAn_History);
            _db.SaveChanges();
        }
        private void createBenhAn_backup()
        {
            BenhAnBackupType benhan_backup = new BenhAnBackupType()
            {
                MABENHAN = Benhan.MABENHAN,
                MABENHNHAN = (int)Benhan.MABENHNHAN,
                MADICHVU = (int)Benhan.MADICHVU,
                NGAYKHAM = (DateTime)Benhan.NGAYKHAM,
                TRIEUCHUNG = Benhan.TRIEUCHUNG,
                KETLUAN = Benhan.KETLUAN,
                MAYSI = (int)Benhan.MAYSI,
                THANHTIEN = (int)Benhan.THANHTIEN,
            };
            Benhan_history = benhan_backup;
        }
        public ChangeHealthRecordViewModel(int maBenhAn)
        {
            findBenhAn(maBenhAn);
            createBenhAn_backup();
            loadDichvu();
            loadBacsi();
            //Bacsi = Benhan.YSI.SUB_ID + ": " + Benhan.YSI.HOTEN;
            Bacsi = Benhan.YSI.HOTEN + ": " + Benhan.YSI.SUB_ID;
            Dichvu = Benhan.DICHVU.SUB_ID + ": " + Benhan.DICHVU.TENDICHVU;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteConfirmChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteConfirmChangeCommand(object? obj)
        {
            //
           // Application.Current.MainWindow.Close();
            try
            {
                update();
                createHistory();
                new MessageBoxCustom("Thông báo", "Sửa thông tin bệnh án thành công!",
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
                //MessageBox.Show(err.Message);
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
    }
}
