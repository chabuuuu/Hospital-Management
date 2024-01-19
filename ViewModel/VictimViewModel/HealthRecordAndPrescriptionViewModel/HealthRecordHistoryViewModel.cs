using LTTQ_DoAn.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static LTTQ_DoAn.ViewModel.HomeViewModel;

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
        public int MABENHNHAN { get; set; }
        public int MADICHVU { get; set; }
        
        public DateTime CHANGED_DATE { get; set; }
    }
    public class HealthRecordHistoryViewModel : BaseViewModel
    {
        private BENHAN benhan;
        private BenhAnHistoryType benhanhistory;
        private List<BenhAnHistoryType> benhanhistorylist;
        private List<BENHAN_HISTORY> benhanHistory;
        public HealthRecordHistoryViewModel() { }
        public ICommand CancelCommand { get; }
        public ICommand RecoveryCommand { get; }

        public HealthRecordHistoryViewModel(int MaBenhAnSelected) { 
            findBenhAn(MaBenhAnSelected);
            load();
            findBenhAn_History();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            RecoveryCommand = new ViewModelCommand(ExecuteRecoveryCommand, CanExecuteRecoveryCommand);
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
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        private void update()
        {
            BENHAN updateBenhan = (from m in _db.BENHAN
                                   where m.MABENHAN == Benhanhistory.MABENHAN
                                   select m).Single();
            updateBenhan.MAYSI = convertBacsiSub_ID(Benhanhistory.MAYSI);
            updateBenhan.MABENHNHAN = Benhanhistory.MABENHNHAN;
            updateBenhan.MADICHVU = Benhanhistory.MADICHVU;
            updateBenhan.THANHTIEN = Decimal.Parse(Benhanhistory.THANHTIEN);
            updateBenhan.TRIEUCHUNG = Benhanhistory.TRIEUCHUNG;
            updateBenhan.KETLUAN = Benhanhistory.KETLUAN;
            _db.SaveChanges();
        }
        
        private void createHistory()
        {
            BENHAN_HISTORY newBenhAn_History = new BENHAN_HISTORY()
            {
                MABENHAN = Benhan.MABENHAN,
                MAYSI = Benhan.MAYSI,
                MADICHVU = Benhan.MADICHVU,
                NGAYKHAM = Benhan.NGAYKHAM,
                MABENHNHAN = Benhan.MABENHNHAN,
                THANHTIEN = Benhan.THANHTIEN,
                TRIEUCHUNG = Benhan.TRIEUCHUNG,
                KETLUAN = Benhan.KETLUAN,
                CHANGED_DATE = DateTime.Now,
            };
            _db.BENHAN_HISTORY.AddObject(newBenhAn_History);
            _db.SaveChanges();
        }
        private bool CanExecuteRecoveryCommand(object obj)
        {
            return true;
        }

        private void ExecuteRecoveryCommand(object obj)
        {
            try{
                createHistory();
                update();
                new MessageBoxCustom("Thông báo", "Đã khôi phục bệnh án bị thay đổi vào: " 
                    + Benhanhistory.CHANGED_DATE.ToString() 
                    + " thành công",
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close();
            }catch(Exception ex)
            {
                new MessageBoxCustom("Lỗi", ex.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
                //MessageBox.Show(err.Message);
                Application.Current.MainWindow.Close();
            }
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
                    MABENHNHAN = (int)item.MABENHNHAN,
                    MADICHVU = (int)item.MADICHVU,
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

        public BenhAnHistoryType Benhanhistory
        {
            get => benhanhistory; set
            {
                benhanhistory = value;
                OnPropertyChanged(nameof(Benhanhistory));
            }
        }
    }
}
