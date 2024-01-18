using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.ViewModel;
using System.Globalization;
using System.Net.NetworkInformation;
using LTTQ_DoAn.View;

namespace LTTQ_DoAn.ViewModell
{
    public class AddHeathRecordViewModel : BaseViewModel
    {
        private string bieuhien;
        private string ketluan;
        private string chiphi;
        private string tenbenhnhan;
        private string bacsi;
        private string dichvu;
        private BENHNHAN benhnhan;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        private List<String> dichvulist;
        private List<String> bacsilist;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public string Bieuhien
        {
            get => bieuhien; set
            {
                bieuhien = value;
                OnPropertyChanged(nameof(Bieuhien));
            }
        }
        public string Ketluan { get => ketluan; set
            {
                ketluan = value;
                OnPropertyChanged(nameof(Ketluan));
            }
        }
        public string Chiphi { get => chiphi; set {
                chiphi = value;
                OnPropertyChanged(nameof(Chiphi));
            } }

        public string TenBenhNhan
        {
            get => tenbenhnhan; set
            {
                tenbenhnhan = value;
                OnPropertyChanged(nameof(TenBenhNhan));
            }
        }
        public List<String> DichVuList
        {
            get => dichvulist; set
            {
                dichvulist = value;
                OnPropertyChanged(nameof(DichVuList));
            }
        }

        public List<string> Bacsilist { get => bacsilist; set
            {
                bacsilist = value;
                OnPropertyChanged(nameof(Bacsilist));
            }
        }

        public string Bacsi { get => bacsi; set
            {
                bacsi = value;
                OnPropertyChanged(nameof(Bacsi));
            }
        }

        public string Dichvu { get => dichvu; set
            {
                dichvu = value;
                OnPropertyChanged(nameof(Dichvu));
                UpdateChiphi(Dichvu);
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
        public BENHNHAN Benhnhan
        {
            get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
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
        public void insert()
        {
            BENHAN newBenhAn = new BENHAN()
            {
                MAYSI = convertBacsiSub_ID(Bacsi),
                MADICHVU = convertDichvuSub_ID(Dichvu),
                NGAYKHAM = DateTime.Now,
                MABENHNHAN = Benhnhan.MABENHNHAN,
                THANHTIEN = Decimal.Parse(Chiphi),
                TRIEUCHUNG = Bieuhien,
                KETLUAN = Ketluan,
            };
            _db.BENHAN.AddObject(newBenhAn);
            _db.SaveChanges();
        }
        public AddHeathRecordViewModel(BENHNHAN SelectedBenhNhan)
        {
            Benhnhan = SelectedBenhNhan;
            loadBacsi();
            loadDichvu();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
           // try
           // {
                insert();
                //MessageBox.Show("Thêm bệnh nhân mới thành công!");
                new MessageBoxCustom("Thông báo", "Thêm bệnh án mới thành công cho bệnh nhân: \n" 
                    + Benhnhan.SUB_ID + ": " + Benhnhan.HOTEN,
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            //}
            //catch (Exception err)
            //{
          /*      //MessageBox.Show(err.Message);
                new MessageBoxCustom("Lỗi", err.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
          */
           // }
        }

    }
}
