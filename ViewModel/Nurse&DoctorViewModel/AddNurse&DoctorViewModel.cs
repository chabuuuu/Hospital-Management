using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;

namespace LTTQ_DoAn.ViewModel
{
    public class AddNurseAndDoctorViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public string Ten { get => ten; set
            {
                ten = value;
                OnPropertyChanged(nameof(Ten));
            }
        }
        public string Gioitinh { get => gioitinh; set
            {
                gioitinh = value;
                OnPropertyChanged(nameof(Gioitinh));
            }
            }
        public string Ngaysinh { get => ngaysinh; set {
                ngaysinh = value;
                OnPropertyChanged(nameof(Ngaysinh));
            } }
        public string Khoa { get => khoa; set {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
            } }
        public string Chucvu { get => chucvu; set {
                chucvu = value;
                OnPropertyChanged(nameof(Chucvu));
            } }
        public string Ngayvaolam { get => ngayvaolam; set {
                ngayvaolam = value;
                OnPropertyChanged(nameof(Ngayvaolam));
            } }
        public string Chihuy { get => chihuy; set {
                chihuy = value;
                OnPropertyChanged(nameof(Chihuy));
            } }

        public string Maphong { get => maphong; set
            {
                maphong = value;
                OnPropertyChanged(nameof(Maphong));
            }
        }

        private string ten;
        private string gioitinh;
        private string ngaysinh;
        private string khoa;
        private string chucvu;
        private string ngayvaolam;
        private string chihuy;
        private string maphong;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private List<String> khoaList;
        public List<String> KhoaList
        {
            get => khoaList; set
            {
                khoaList = value;
                OnPropertyChanged(nameof(KhoaList));
            }
        }
        public void loadKhoa()
        {
            _db = new QUANLYBENHVIENEntities();
            List<KHOA> khoa = _db.KHOA.ToList();
            List<String> subID = new List<String>();
            foreach (var item in khoa)
            {
                subID.Add(item.SUB_ID + ": " + item.TENKHOA);
            }
            this.KhoaList = subID;
        }

        public int? convertChiHuySUB_ID(string Sub_id)
        {
            if (Sub_id == null)
            {
                return null;
            }
            // Chuỗi cần tách
            string inputString = Sub_id;

            // Tách các ký tự còn lại thành một chuỗi riêng
            string remainingCharacters = inputString.Substring(1);

            return int.Parse(remainingCharacters);
        }
        public int? convertPhongSUB_ID(string Sub_id)
        {
            if (Sub_id == null)
            {
                return null;
            }
            // Chuỗi cần tách
            string inputString = Sub_id;

            // Tách các ký tự còn lại thành một chuỗi riêng
            string remainingCharacters = inputString.Substring(3);

            return int.Parse(remainingCharacters);
        }
        public int convertKhoaSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        public void insert()
        {
            YSI newYsi = new YSI()
            {
                HOTEN = Ten,
                GIOITINH = Gioitinh,
                NGAYSINH = DateTime.ParseExact(Ngaysinh, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                MAPHONG = this.convertPhongSUB_ID(Maphong),
                NGAYVAOLAM = DateTime.ParseExact(Ngayvaolam, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                MACHIHUY = this.convertChiHuySUB_ID(Chihuy),
                MAKHOA = this.convertKhoaSub_ID(Khoa),
            };
            _db.YSI.Add(newYsi);
            _db.SaveChanges();
        }
        public AddNurseAndDoctorViewModel()
        {
            loadKhoa();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }
        //hành động của nút hủy bỏ: đóng cửa sổ
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        //điều kiện để lệnh hủy bỏ được thực hiện: k có điều kiện
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }

        //hành động thêm vào
        private void ExecuteAddCommand(object? obj)
        {
            //câu lệnh thêm ở đây
            try
            {
                insert();
                MessageBox.Show("Thêm y sĩ mới thành công!");
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
        //điều kiện để lệnh thêm được thực hiện: lich khám không có sẵn trong database
        private bool CanExecuteAddCommand(object? obj)
        {
            // những điều kiện cần xét

            // nếu không thỏa sẽ show messagebox rằng đã có trong lịch khám và return false
            return true; // khi nào thỏa điều kiện sẽ chấp nhận
        }
    }
}
