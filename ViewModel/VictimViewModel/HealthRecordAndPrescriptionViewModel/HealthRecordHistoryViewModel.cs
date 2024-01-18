using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class BenhAnHistoryType
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
        
        public DateTime CHANGED_DATE { get; set; }
    }
    public class HealthRecordHistoryViewModel : BaseViewModel
    {
        private BENHAN benhan;
        private List<BenhAnHistoryType> benhanhistorylist;
        private List<BENHAN_HISTORY> benhanHistory;
        public HealthRecordHistoryViewModel() { }
        public ICommand CancelCommand { get; }

        public HealthRecordHistoryViewModel(int MaBenhAnSelected) { 
            findBenhAn(MaBenhAnSelected);
            load();
            findBenhAn_History();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);

        }

        private bool CanExecuteCancelCommand(object obj)
        {
            return true;
        }

        private void ExecuteCancelCommand(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        private void load()
        {
            BenhanHistory = Benhan.BENHAN_HISTORY.ToList();
        }

        private void findBenhAn(int maBenhAn)
        {

            BENHAN benhAn = (from m in _db.BENHAN
                             where m.MABENHAN == maBenhAn
                             select m).First();
            Benhan = benhAn;
        }
        public BENHAN Benhan
        {
            get => benhan; set
            {
                benhan = value;
                OnPropertyChanged(nameof(Benhan));
            }
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
        private void findBenhAn_History()
        {
            List<BenhAnHistoryType> list = new List<BenhAnHistoryType>();
            foreach (var item in BenhanHistory)
            {
                BenhAnHistoryType benhan_history = new BenhAnHistoryType()
                {
                    MABENHAN = (int)item.MABENHAN,
                    SUB_ID = "BA" + ((int)item.MABENHAN).ToString(),
                    NGAYKHAM = item.NGAYKHAM.ToString(),
                    KHOA = findKhoa(item.MAYSI),
                    TRIEUCHUNG = item.TRIEUCHUNG,
                    KETLUAN = item.KETLUAN,
                    MAYSI = findBacSi(item.MAYSI),
                    DICHVU = findDichVu(item.MADICHVU),
                    THANHTIEN = item.THANHTIEN.ToString(),
                    CHANGED_DATE = (DateTime)item.CHANGED_DATE,
                };
                list.Add(benhan_history);
            }
            Benhanhistorylist = list;
        }
        public List<BENHAN_HISTORY> BenhanHistory
        {
            get => benhanHistory; set
            {
                benhanHistory = value;
                OnPropertyChanged(nameof(BenhanHistory));
            }
        }

        public List<BenhAnHistoryType> Benhanhistorylist
        {
            get => benhanhistorylist; set
            {
                benhanhistorylist = value;
                OnPropertyChanged(nameof (Benhanhistorylist));
            }
        }
    }
}
