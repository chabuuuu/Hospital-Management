using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;

namespace LTTQ_DoAn.ViewModel
{
    public class ViewFieldViewModel : BaseViewModel
    {
        private KHOA khoa;
        private string image_url;
        private string truongkhoa;
        private string soluongysi;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public ICommand CancelCommand { get; }
        public KHOA Khoa
        {
            get => khoa; set
            {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
            }
        }

        public string Truongkhoa { get => truongkhoa; set => truongkhoa = value; }
        public string Soluongysi { get => soluongysi; set => soluongysi = value; }
        public string Image_url { get => image_url; set => image_url = value; }

        public void loadSoLuong()
        {
            int soBacSi = 0;
            int soYTa = 0;
            int soNhanVienYTe = 0;
            List<YSI> bacsi = _db.YSI.ToList();
            foreach (var item in bacsi)
            {
                if (item.LOAIYSI == null || item.MAKHOA != Khoa.MAKHOA)
                {
                    continue;
                }
                if (item.LOAIYSI.Substring(0, 6).Equals("Bác sĩ"))
                {
                    soBacSi++;
                    continue;
                }
                if (item.LOAIYSI.Substring(0, 4).Equals("Y tá"))
                {
                    soYTa++;
                    continue;
                }
                soNhanVienYTe++;
            }
            Soluongysi = "Bác sĩ: " + soBacSi + 
                "\nY tá: " + soYTa + 
                "\nNhân viên y tế: " + soNhanVienYTe;

        }
        private void FindTruongKhoa()
        {
            if (Khoa.TRUONGKHOA == null)
            {
                Truongkhoa = "Chưa có trưởng khoa, hãy cập nhật ngay";
                return;
            }
            string? tenBacSi = (from m in _db.YSI
                                where m.MAYSI == Khoa.TRUONGKHOA
                                select m.HOTEN).First();
            if (tenBacSi == null)
            {
                return;
            }
            Truongkhoa = "M" + Khoa.TRUONGKHOA.ToString() + ": " + tenBacSi;
        }
        public ViewFieldViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        public ViewFieldViewModel(KHOA SelectedKhoa)
        {
            Khoa = SelectedKhoa;
            Image_url = SelectedKhoa.PICTURE;
            loadSoLuong();
            FindTruongKhoa();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }

        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;//ko điều kiện
        }

        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
