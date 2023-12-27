using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class ViewAppointmentViewModel : BaseViewModel
    {
        private LICHKHAM lichkham;
        private DICHVU dichvu;
        private BENHNHAN benhnhan;
        private string phong;
        private string bacsi;
        public ICommand CancelCommand { get; }
        public LICHKHAM Lichkham { get => lichkham; set => lichkham = value; }
        public DICHVU Dichvu { get => dichvu; set => dichvu = value; }
        public BENHNHAN Benhnhan { get => benhnhan; set => benhnhan = value; }
        public string Bacsi { get => bacsi; set => bacsi = value; }
        public string Phong { get => phong; set => phong = value; }

        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        private void findBacSi()
        {
            string? tenBacSi = (from m in _db.YSI
                               where m.MAYSI == Lichkham.MABACSI
                               select m.HOTEN).First();
            if (tenBacSi == null)
            {
                return;
            }
            Bacsi = "M" + Lichkham.MABACSI.ToString() + ": " + tenBacSi;
        }
        public ViewAppointmentViewModel(LICHKHAM SeletedLichKham, DICHVU SelectedDichVu, BENHNHAN SelectedBenhNhan)
        {
            Lichkham = SeletedLichKham;
            Dichvu = SelectedDichVu;
            Benhnhan = SelectedBenhNhan;
            findBacSi();
            Phong = "PHG" + SeletedLichKham.MAPHONG.ToString();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        public ViewAppointmentViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
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
