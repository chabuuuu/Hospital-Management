using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;

namespace LTTQ_DoAn.ViewModel
{
    public class ViewNurseAndDoctorViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private int tuoi;
        private string maphong;
        private string khoa;
        private YSI ysi;
        private string captren;
        private string ngaysinh;
        private string ngayvaolam;
        public ICommand CancelCommand { get; }
        public YSI Ysi { get => ysi; set => ysi = value; }
        public int Tuoi { get => tuoi; set => tuoi = value; }
        public string Maphong { get => maphong; set => maphong = value; }
        public string Khoa { get => khoa; set => khoa = value; }
        public string Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Captren { get => captren; set => captren = value; }
        public string Ngayvaolam { get => ngayvaolam; set => ngayvaolam = value; }

        public ViewNurseAndDoctorViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        public ViewNurseAndDoctorViewModel(YSI SelectedYsi)
        {
            Ysi = SelectedYsi;
            findKhoa();
            findTruongPhong();
            convertDateTimeFormat();
            convertMaPhong();
            convertTuoi();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        private void convertTuoi()
        {
            DateTime ngayHienTai = DateTime.Now;

            // Lấy ra năm từ ngày hiện tại
            int namHienTai = ngayHienTai.Year;

            DateTime ngaySinh = (DateTime)Ysi.NGAYSINH;
            int namSinh = ngaySinh.Year;
            Tuoi = namHienTai - namSinh;
        }
        void convertDateTimeFormat()
        {
            DateTime notNullNgaysinh = (DateTime)Ysi.NGAYSINH;
            Ngaysinh = notNullNgaysinh.ToString("dd/MM/yyyy");

            DateTime notNullNgayvaolam = (DateTime)Ysi.NGAYVAOLAM;
            Ngayvaolam = notNullNgayvaolam.ToString("dd/MM/yyyy");
        }
        void convertMaPhong()
        {
            Maphong = "PHG" + Ysi.MAPHONG.ToString();
        }
        void findKhoa()
        {
            string? tenKhoa = (from m in _db.KHOA
                           where m.MAKHOA == Ysi.MAKHOA
                           select m.TENKHOA).First();
            if (tenKhoa == null)
            {
                return;
            }
            Khoa = "K" + Ysi.MAKHOA.ToString() + ": " + tenKhoa;
        }
        void findTruongPhong()
        {
            string? tenCapTren = (from m in _db.YSI
                               where m.MAYSI == Ysi.MACHIHUY
                               select m.HOTEN).FirstOrDefault();
            if (tenCapTren == null)
            {
                Captren = "Không có cấp trên";
                return;
            }
            Captren = "M" + Ysi.MACHIHUY.ToString() + ": " + tenCapTren;
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
    }
}
