using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using System.Windows.Input;
using LTTQ_DoAn.View;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeAppointmentViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private string benhnhan;
        private string bacsi;
        private string dichvu;
        private string phong;
        private List<String> benhnhanList;
        private List<string> bacsiList;
        private List<String> dichvuList;
        private List<String> phongList;

        private LICHKHAM lichkham;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public LICHKHAM Lichkham { get => lichkham; set
            {
                lichkham = value;
                OnPropertyChanged(nameof(Lichkham));
            }
        }

        public string Benhnhan { get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
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
            }
        }
        public string Phong
        {
            get => phong; set
            {
                phong = value;
                OnPropertyChanged(nameof(Phong));
            }
        }
        public List<string> BenhnhanList { get => benhnhanList; set => benhnhanList = value; }
        public List<string> BacsiList { get => bacsiList; set => bacsiList = value; }
        public List<string> DichvuList { get => dichvuList; set => dichvuList = value; }
        public List<string> PhongList { get => phongList; set => phongList = value; }

        public void loadBenhnhan()
        {
            List<BENHNHAN> benhnhan = _db.BENHNHAN.ToList();
            List<String> subID = new List<String>();
            foreach (var item in benhnhan)
            {
                subID.Add(item.SUB_ID + ": " + item.HOTEN);
                if (item.MABENHNHAN == Lichkham.MABENHNHAN)
                {
                    Benhnhan = item.SUB_ID + ": " + item.HOTEN;
                }
            }
            this.BenhnhanList = subID;
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
                    subID.Add(item.SUB_ID + ": " + item.HOTEN);
                    if (item.MAYSI == Lichkham.MABACSI)
                    {
                        Bacsi = item.SUB_ID + ": " + item.HOTEN;
                    }
                }
            }
            this.BacsiList = subID;

        }

        public void loadDichvu()
        {
            List<DICHVU> dichvu = _db.DICHVU.ToList();
            List<String> subID = new List<String>();
            foreach (var item in dichvu)
            {
                subID.Add(item.SUB_ID + ": " + item.TENDICHVU);
                if (item.MADICHVU == Lichkham.MADICHVU)
                {
                    Dichvu = item.SUB_ID + ": " + item.TENDICHVU;
                }
            }
            this.DichvuList = subID;
        }
        public int convertBacsiSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        public int convertBenhnhanSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(2);
            return int.Parse(k1);
        }
        public int convertDichvuSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(2);
            return int.Parse(k1);
        }
        public int? convertPhongSUB_ID(string Sub_id)
        {
            if (Sub_id == null)
            {
                return null;
            }
            // Chuỗi cần tách
            string inputString = Sub_id;
            string[] parts = inputString.Split(new[] { ':' }, 2);
            // Tách các ký tự còn lại thành một chuỗi riêng
            string remainingCharacters = parts[0].Substring(3);
            return int.Parse(remainingCharacters);
        }
        public void loadPhong()
        {
            List<PHONG> phong = _db.PHONG.ToList();
            List<String> subID = new List<String>();
            foreach (var item in phong)
            {
                subID.Add(item.SUB_ID + ": " + item.TENPHONG);
            }
            this.PhongList = subID;
        }
        private void update()
        {
            LICHKHAM updateLichkham = (from m in _db.LICHKHAM
                                       where m.MALICHKHAM == Lichkham.MALICHKHAM
                                       select m).Single();
            updateLichkham.MABENHNHAN = convertBenhnhanSub_ID(Benhnhan);
            updateLichkham.MABACSI = convertBacsiSub_ID(Bacsi);
            updateLichkham.MAPHONG = convertPhongSUB_ID(Phong);
            updateLichkham.MADICHVU = convertDichvuSub_ID(Dichvu);
            updateLichkham.MAPHONG = convertPhongSUB_ID(Phong);
            updateLichkham.NGAYLENLICH = Lichkham.NGAYLENLICH;
            updateLichkham.NGAYKHAM = Lichkham.NGAYKHAM;
            _db.SaveChanges();
        }

        public ChangeAppointmentViewModel(LICHKHAM SelectedLichKham)
        {
            Lichkham = SelectedLichKham;
            loadBacsi();
            loadBenhnhan();
            loadDichvu();
            loadPhong();
            Phong = "PHG" + Lichkham.MAPHONG.ToString() + ": " + Lichkham.PHONG.TENPHONG;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        public ChangeAppointmentViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }

        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        //điều kiện để lệnh hủy bỏ được thực hiện: k có điều kiện
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        //---------------------------------------------
        private void ExecuteConfirmChangeCommand(object? obj)
        {
            try
            {
                update();
                //MessageBox.Show("Sửa thông tin y sĩ thành công!");
                new MessageBoxCustom("Thành công", "Sửa thông tin lịch khám thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
                //MessageBox.Show(err.Message);
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
        private bool CanExecuteConfirmChangeCommand(object? obj)
        {
            return true;
        }

    }
}
