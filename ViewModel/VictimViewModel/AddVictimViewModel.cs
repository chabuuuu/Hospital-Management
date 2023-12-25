using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//thêm 3 thư viện mới này
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.View;
using System.Globalization;

namespace LTTQ_DoAn.ViewModel
{
    public class AddVictimViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private int id;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public int Id { get => id; set => id = value; }
        public string Ten
        {
            get => ten; set
            {
                ten = value;
                OnPropertyChanged(nameof(Ten));
            }
        }
        public string Ngaysinh
        {
            get => ngaysinh; set
            {
                ngaysinh = value;
                OnPropertyChanged(nameof(Ngaysinh));
            }
        }
        public string Gioitinh
        {
            get => gioitinh; set
            {
                gioitinh = value;
                OnPropertyChanged(nameof(Gioitinh));
            }
        }
        public string Bhyt
        {
            get => bhyt; set
            {
                bhyt = value;
                OnPropertyChanged(nameof(Bhyt));
            }
        }
        public string Maphong
        {
            get => maphong; set
            {
                maphong = value;
                OnPropertyChanged(nameof(Maphong));
            }
        }
        public string Ngaynhapvien
        {
            get => ngaynhapvien; set
            {
                ngaynhapvien = value;
                OnPropertyChanged(nameof(Ngaynhapvien));
            }
        }
        public string Diachi
        {
            get => diachi; set
            {
                diachi = value;
                OnPropertyChanged(nameof(Diachi));
            }
        }

        private string ten = null;
        private string ngaysinh = null;
        private string gioitinh = null;
        private string bhyt = null;
        private string maphong = null;
        private string ngaynhapvien = null;
        private string diachi = null;
        public AddVictimViewModel(int id)
        {
            this.Id = id;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }
        public void insert()
        {
            BENHNHAN newBenhnhan = new BENHNHAN()
            {
                HOTEN = this.Ten,
                GIOITINH = Gioitinh,
                NGAYSINH = DateTime.ParseExact(Ngaysinh, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                MABHYT = Bhyt,
                MAPHONG = this.convertSUB_ID(Maphong),
                NGAYNHAPVIEN = DateTime.ParseExact(Ngaynhapvien, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                DIACHI = Diachi
            };
            //newBenhnhan.MAPHONG = 1;
            //MessageBox.Show(Gioitinh);
            //MessageBox.Show(Ten + Gioitinh + Ngaysinh + Bhyt + this.convertSUB_ID(Maphong) + Ngaynhapvien + Diachi);
            _db.BENHNHAN.Add(newBenhnhan);
            //_db.BENHNHAN.AddObject(newBenhnhan);
            _db.SaveChanges();
            //BaseViewModel.global_db = _db;
        }
        public AddVictimViewModel()
        {

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
        public int convertSUB_ID(string Sub_id)
        {
            // Chuỗi cần tách
            string inputString = Sub_id;

            // Tách các ký tự còn lại thành một chuỗi riêng
            string remainingCharacters = inputString.Substring(3);

            return int.Parse(remainingCharacters);
        }
        //hành động thêm vào
        private void ExecuteAddCommand(object? obj)
        {
            //câu lệnh thêm ở đây
            try
            {
                insert();
                //MessageBox.Show("Thêm bệnh nhân mới thành công!");
                new MessageBoxCustom("Thông báo", "Thêm bệnh nhân mới thành công!", 
                    MessageType.Success, 
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            } catch (Exception err)
            {
                //MessageBox.Show(err.Message);
                new MessageBoxCustom("Lỗi", err.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
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
