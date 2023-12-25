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
    public class ViewVictimViewModel : BaseViewModel
    {
        private BENHNHAN benhnhan = null;
        private int tuoi;
        private string maphong;
        private string khoa;
        //private string ngaynhapvien
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private string ngaysinh;

        public ICommand CancelCommand { get; }
        public BENHNHAN Benhnhan { get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        public int Tuoi { get => tuoi; set
            {
                tuoi = value;
                OnPropertyChanged(nameof(Tuoi));
            }
        }

        public string Ngaysinh { get => ngaysinh; set
            {
                ngaysinh = value;
                OnPropertyChanged(nameof(Ngaysinh));
            }
        }

        public string Maphong { get => maphong; set
            {
                maphong = value;
                OnPropertyChanged(nameof(Maphong));
            }
        }

        public string Khoa { get => khoa; set
            {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
            }
        }

        private void convertTuoi()
        {
            DateTime ngayHienTai = DateTime.Now;

            // Lấy ra năm từ ngày hiện tại
            int namHienTai = ngayHienTai.Year;

            DateTime ngaySinh =  (DateTime) Benhnhan.NGAYSINH ;
            int namSinh = ngaySinh.Year;
            Tuoi = namHienTai - namSinh;
        }
        void convertDateTimeFormat()
        {
            DateTime notNullNgaysinh = (DateTime)Benhnhan.NGAYSINH;
            Ngaysinh = notNullNgaysinh.ToString("dd/MM/yyyy");
        }
        void convertMaPhong()
        {
            Maphong = "PHG" + Benhnhan.MAPHONG.ToString();
        }

        void findKhoa()
        {
            var BenhNhan_Lichkham_Ysi =
                (from ys in _db.YSI
                 from bn in _db.BENHNHAN
                 join lk in _db.LICHKHAM on bn.MABENHNHAN equals lk.MABENHNHAN
                 where ys.MAYSI == lk.MABACSI
                 orderby lk.NGAYKHAM descending
                 select new
                 {
                     MaBenhNhan = Benhnhan.MABENHNHAN,
                     Khoa = ys.MAKHOA, 
                 }).ToList();
            int? maKhoa = (from m in BenhNhan_Lichkham_Ysi
             where m.MaBenhNhan == Benhnhan.MABENHNHAN
             select m).First().Khoa;
            if (maKhoa == null)
            {
                return;
            }
            string? tenKhoa = (from k in _db.KHOA
                              where k.MAKHOA == maKhoa
                              select k.TENKHOA).Single();
            Khoa = tenKhoa;
        }

        public ViewVictimViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        public ViewVictimViewModel(BENHNHAN SelectedBenhNhan)
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            Benhnhan = SelectedBenhNhan;
            convertDateTimeFormat();
            convertTuoi();
            convertMaPhong();
            findKhoa();
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
