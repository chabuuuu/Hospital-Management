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
using System.Globalization;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeAppointmentViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private string benhnhan;
        private string bacsi;
        private string dichvu;
        private string phong;
        private string ngaylenlich;
        private string ngaykham;
        private string cakham;
        private List<String> benhnhanList;
        private List<string> bacsiList;
        private List<String> dichvuList;
        private List<String> phongList;

        private LICHKHAM lichkham;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public string Cakham
        {
            get => cakham; set
            {
                cakham = value;
                OnPropertyChanged(nameof(Cakham));
            }
        }
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
        public string Ngaylenlich
        {
            get => ngaylenlich; set
            {
                ngaylenlich = value;
                OnPropertyChanged(nameof(Ngaylenlich));
            }
        }
        public string Ngaykham
        {
            get => ngaykham; set
            {
                ngaykham = value;
                OnPropertyChanged(nameof(Ngaykham));
            }
        }
        public List<string> BenhnhanList { get => benhnhanList; set => benhnhanList = value; }
        public List<string> BacsiList { get => bacsiList; set => bacsiList = value; }
        public List<string> DichvuList { get => dichvuList; set => dichvuList = value; }
        public List<string> PhongList { get => phongList; set => phongList = value; }

        public void checkCaKham()
        {
            int idBacSi = convertBacsiSub_ID(Bacsi);
            YSI thisYsi = (from m in _db.YSI
                           where m.MAYSI == idBacSi
                           select m).FirstOrDefault();
            if (thisYsi == null)
            {
                throw new Exception("Bác sĩ này không tồn tại!");
            }
            int Ca = int.Parse(Cakham);
            DateTime new_NgayKham = (DateTime)Lichkham.NGAYKHAM;
            LICHKHAM check_trung_lich_kham = thisYsi.LICHKHAM.Where(i => i.CAKHAM == Ca && i.NGAYKHAM == new_NgayKham).FirstOrDefault();
            if (check_trung_lich_kham != null && Lichkham.MABENHNHAN != convertBenhnhanSub_ID(Benhnhan))
            {
                throw new Exception("Bác sĩ này đã có lịch khám vào ca " + Cakham + " rồi!");
            }
            int new_MaPhong = (int)convertPhongSUB_ID(Phong);
            LICHKHAM check_trung_voi_bacsi_khac = _db.LICHKHAM.Where(k => k.CAKHAM == Ca &&
                k.NGAYKHAM == new_NgayKham &&
                k.MAPHONG == new_MaPhong &&
                k.MABACSI != idBacSi &&
                k.MABACSI != Lichkham.MABACSI
                ).SingleOrDefault();
            if (check_trung_voi_bacsi_khac != null)
            {
                throw new Exception("Ca khám vào phòng này đã được đặt trước cho bác sĩ khác");
            }
            return;
        }

        public void loadBenhnhan()
        {
            List<BENHNHAN> benhnhan = _db.BENHNHAN.ToList();
            List<String> subID = new List<String>();
            foreach (var item in benhnhan)
            {
                subID.Add(item.HOTEN + ": " + item.SUB_ID);
                if (item.MABENHNHAN == Lichkham.MABENHNHAN)
                {
                    Benhnhan = item.HOTEN + ": " + item.SUB_ID;
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
                    subID.Add(item.HOTEN + ": " + item.SUB_ID);
                    if (item.MAYSI == Lichkham.MABACSI)
                    {
                        Bacsi = item.HOTEN + ": " + item.SUB_ID;
                    }
                }
            }
            this.BacsiList = subID;

        }
        public void loadCakham()
        {
            Cakham = Lichkham.CAKHAM.ToString();
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
            string k1 = parts[1].Substring(2);
            return int.Parse(k1);
        }
        public int convertBenhnhanSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[1].Substring(3);
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
            int update_Ca = int.Parse(Cakham);
            checkCaKham();
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
            updateLichkham.CAKHAM = update_Ca;
            _db.SaveChanges();
        }

        public ChangeAppointmentViewModel(LICHKHAM SelectedLichKham)
        {
            Lichkham = SelectedLichKham;
            loadBacsi();
            loadBenhnhan();
            loadDichvu();
            loadPhong();
            loadCakham();
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
