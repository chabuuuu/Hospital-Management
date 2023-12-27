using LTTQ_DoAn.Model;
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

    }
    public class HealthRecordViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private string khoa;
        private HealthRecordAndPrescription thisView;
        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand ExitCommand { get; }
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

        private BENHNHAN benhnhan;
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
                THANHTIEN = item.THANHTIEN.ToString() };
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
        
        public HealthRecordViewModel()
        {
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
        }
        public HealthRecordViewModel(BENHNHAN SelectedBenhNhan, HealthRecordAndPrescription view)
        {
            this.thisView = view;
            Benhnhan = SelectedBenhNhan;
            findBenhAn();
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
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

        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
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

        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj)
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
                    e.Message + "\nLỗi: " + e.GetType().ToString(),
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeCommand(object? obj)
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
    }
}
