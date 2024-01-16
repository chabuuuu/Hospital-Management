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
using MaterialDesignThemes.Wpf;

namespace LTTQ_DoAn.ViewModell
{
    public class AddPrescriptionViewModel : BaseViewModel
    {
        private string tenbenhnhan;
        private string tenbacsi;
        private string mabenhan;
        private string mathuoc;
        private string ghichu;
        private int solan;
        private int soluong;
        private int madonthuoc_recent_inserted;
        private BENHNHAN benhnhan;
        private List<InsertThuoc> insertThuocList = new List<InsertThuoc>();
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private List<String> benhanlist;
        private List<String> thuoclist;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public ICommand AddMedicineCommand { get; }
        public class InsertThuoc
        {
            public int Mathuoc { get; set; }
            public float Soluong { get; set; }
            public int Solan { get; set; }
        }
        public string Tenbenhnhan
        {
            get => tenbenhnhan; set
            {
                tenbenhnhan = value;
                OnPropertyChanged(nameof(Tenbenhnhan));
            }
        }
        public string Tenbacsi
        {
            get => tenbacsi; set
            {
                tenbacsi = value;
                OnPropertyChanged(nameof(Tenbacsi));
            }
        }
        public string Mabenhan
        {
            get => mabenhan; set
            {
                mabenhan = value;
                OnPropertyChanged(nameof(Mabenhan));
            }
        }
        public string Ghichu
        {
            get => ghichu; set
            {
                ghichu = value;
                OnPropertyChanged(nameof(Ghichu));
                //convertGhiChuToCTDT();
            }
        }
        public List<String> BenhAnList
        {
            get => benhanlist; set
            {
                benhanlist = value;
                OnPropertyChanged(nameof(BenhAnList));
            }
        }

        public BENHNHAN Benhnhan
        {
            get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        public List<string> Thuoclist
        {
            get => thuoclist; set
            {
                thuoclist = value;
                OnPropertyChanged(nameof(Thuoclist));
            }
        }

        public string Mathuoc
        {
            get => mathuoc; set
            {
                mathuoc = value;
                OnPropertyChanged(nameof(Mathuoc));
            }
        }

        public int Solan
        {
            get => solan; set
            {
                solan = value;
                OnPropertyChanged(nameof(Solan));
            }
        }
        public int Soluong
        {
            get => soluong; set
            {
                soluong = value;
                OnPropertyChanged(nameof(Soluong));
            }
        }

        public List<InsertThuoc> InsertThuocList
        {
            get => insertThuocList; set
            {
                insertThuocList = value;
                OnPropertyChanged(nameof(InsertThuocList));
            }
        }

        public int Madonthuoc_recent_inserted
        {
            get => madonthuoc_recent_inserted; set
            {
                madonthuoc_recent_inserted = value;
                OnPropertyChanged(nameof(Madonthuoc_recent_inserted));
            }
        }

         public void insertThuoc()
         {
            if (Madonthuoc_recent_inserted == null)
            {
                throw new Exception("Mã đơn thuốc không hợp lệ");
            }
             foreach (var item in InsertThuocList)
             {
                 CHITIETDONTHUOC newCTDT = new CHITIETDONTHUOC()
                 {
                     MADONTHUOC = Madonthuoc_recent_inserted,
                     MATHUOC = item.Mathuoc,
                     SOLAN = item.Solan,
                     SOLUONG = item.Soluong,
                 };
                 _db.CHITIETDONTHUOC.AddObject(newCTDT);
                 _db.SaveChanges();
             }
         }
        
        public void loadBenhan()
        {
            //List<BENHAN> benhan = _db.BENHAN.ToList();
            List<BENHAN> benhan = Benhnhan.BENHAN.ToList();

            List<String> subID = new List<String>();
            foreach (var item in benhan)
            {
                if (item.MABENHNHAN == benhnhan.MABENHNHAN)
                    subID.Add(item.SUB_ID);
            }
            this.BenhAnList = subID;
        }
        public void loadThuoc()
        {
            List<THUOC> thuoc = _db.THUOC.ToList();

            List<String> subID = new List<String>();
            foreach (var item in thuoc)
            {
                    subID.Add(item.TENTHUOC + "-" + item.SUB_ID);
            }
            this.Thuoclist = subID;
        }
        public int convertBenhanSub_ID(string inputString)
        {
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(2);
            return int.Parse(k1);
        }
        public int convertThuocSub_ID(string inputString)
        {
            string[] parts = inputString.Split(new[] { '-' }, 2);
            string k1 = parts[1].Substring(3);
            return int.Parse(k1);
        }
        public string getThuoc_Name(string inputString)
        {
            string[] parts = inputString.Split(new[] { '-' }, 2);
            string k1 = parts[0];
            return k1;
        }
        public void insert()
        {
            DONTHUOC newDonThuoc = new DONTHUOC()
            {
                MABENHAN = convertBenhanSub_ID(Mabenhan),
                GHICHU = Ghichu,
            };
            _db.DONTHUOC.AddObject(newDonThuoc);
            _db.SaveChanges();
            Madonthuoc_recent_inserted = newDonThuoc.MADONTHUOC;
        }
        public AddPrescriptionViewModel(BENHNHAN SelectedBenhNhan)
        {
            Benhnhan = SelectedBenhNhan;
            Tenbenhnhan = Benhnhan.HOTEN;
            loadBenhan();
            loadThuoc();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            AddMedicineCommand = new ViewModelCommand(ExecuteAddMedicineCommand, CanExecuteAddMedicineCommand);
        }
        public string getDonViTinh(int mathuoc)
        {
            THUOC thuoc = (from m in _db.THUOC
                           where m.MATHUOC == mathuoc 
                           select m).SingleOrDefault();
            if (thuoc != null)
            {
                return thuoc.DONVITINH;
            }
            return "";
        }
        public THUOC getThuoc(int mathuoc)
        {
            THUOC thuoc = (from m in _db.THUOC
                           where m.MATHUOC == mathuoc
                           select m).SingleOrDefault();
            if (thuoc != null)
            {
                return thuoc;
            }
            return null;
        }
        public void loadToGhiChu()
        {
            THUOC selectThuoc = getThuoc(convertThuocSub_ID(Mathuoc));
            if (selectThuoc == null)
            {
                throw new Exception("Không tìm thấy loại thuốc này");
            }
            string addThuoc = "+ Tên thuốc: " + selectThuoc.TENTHUOC 
                + " - Mã thuốc:" + selectThuoc.SUB_ID + "- Số lượng:" + Soluong.ToString()
                + "- Đơn vị: " + selectThuoc.DONVITINH
                + " - Dùng:" + Solan.ToString() + "*Lần/Ngày "
                +" - Giá: " + selectThuoc.GIATIEN;
            /*InsertThuoc new_insertThuoc = new InsertThuoc()
            {
                Mathuoc = selectThuoc.MATHUOC,
                Soluong = Soluong,
                Solan = Solan
            };
            InsertThuocList.Add(new_insertThuoc);*/
            if (Ghichu == null)
            {
                Ghichu = addThuoc;
            }
            else
            {
                Ghichu = Ghichu + "\n" + addThuoc;
            }
        }
        public string[] RemoveEmptyElements(string[] inputArray)
        {
            // Sử dụng LINQ để lọc các phần tử không rỗng
            string[] newArray = inputArray.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            return newArray;
        }
        public int convertSoLuong(string inputString)
        {
            string new_soLuong_string = inputString.Split(new[] { '-' })[2];
            string[] soLuong_parts = new_soLuong_string.Split(new[] { ':' });
            string soLuong_string = soLuong_parts[1];
            int soLuong = int.Parse(soLuong_string);
            return soLuong;
        }
        public int convertSoLan(string inputString)
        {
            string new_soLan_string = inputString.Split(new[] { '-' })[4];
            string[] soLan_parts = new_soLan_string.Split(new[] { ':' });
            string soLan_string = soLan_parts[1];
            string next_split = soLan_string.Split(new[] { '*' })[0];
            int soLan = int.Parse(next_split);
            return soLan;
        }
        public void convertGhiChuToCTDT()
        {
            if (Ghichu == null)
            {
                InsertThuocList = new List<InsertThuoc>();
                return;
            }
            string[] parts = Ghichu.Split(new[] { '+' });
            string[] new_parts = RemoveEmptyElements(parts);
            List<InsertThuoc> temp_insert_thuoc_list = new List<InsertThuoc>();
            foreach (var item in new_parts)
            {
                string new_maThuoc_string = item.Split(new[] { '-' })[1];
                string[] maThuoc_parts = new_maThuoc_string.Split(new[] { ':' });
                string maThuoc_string = maThuoc_parts[1].Substring(3);
                int maThuoc = int.Parse(maThuoc_string);

                int new_soluong = convertSoLuong(item);
                int new_solan = convertSoLan(item);

                THUOC newThuoc = getThuoc(maThuoc);
                if (newThuoc == null)
                {
                    throw new Exception("Không tìm thấy loại thuốc MED" + maThuoc);
                }
                InsertThuoc new_insertThuoc = new InsertThuoc()
                {
                    Mathuoc = newThuoc.MATHUOC,
                    Soluong = new_soluong,
                    Solan = new_solan
                };
                temp_insert_thuoc_list.Add(new_insertThuoc);
            }
            InsertThuocList = temp_insert_thuoc_list;
        }

        private bool CanExecuteAddMedicineCommand(object obj)
        {
            return true;
        }

        private void ExecuteAddMedicineCommand(object obj)
        {
            try
            {
                loadToGhiChu();
            }
            catch(Exception ex)
            {
                new MessageBoxCustom("Lỗi", "Có lỗi xảy ra"
    + ex.Message,
    MessageType.Error,
    MessageButtons.OK)
    .ShowDialog();
            }
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
            try {
                insert();
                convertGhiChuToCTDT();
                insertThuoc();
            }
            catch (Exception e)
            {
                new MessageBoxCustom("Loi", "Có lỗi xảy ra"
    + e.Message,
    MessageType.Error,
    MessageButtons.OK)
    .ShowDialog();
            }
            new MessageBoxCustom("Thông báo", "Thêm đơn thuốc mới thành công cho bệnh nhân: \n"
                + Benhnhan.SUB_ID + ": " + Benhnhan.HOTEN,
                MessageType.Success,
                MessageButtons.OK)
                .ShowDialog();
            Application.Current.MainWindow.Close();
        }

    }
}
