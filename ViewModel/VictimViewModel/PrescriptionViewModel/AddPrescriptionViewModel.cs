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
        private string ghichu;
        private BENHNHAN benhnhan;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private List<String> benhanlist;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
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
        public void loadBenhan()
        {
            List<BENHAN> benhan = _db.BENHAN.ToList();
            List<String> subID = new List<String>();
            foreach (var item in benhan)
            {
                subID.Add(item.SUB_ID);
            }
            this.BenhAnList = subID;
        }
        public int convertBenhanSub_ID(string inputString)
        {
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(2);
            return int.Parse(k1);
        }
        public void insert()
        {
            DONTHUOC newDonThuoc = new DONTHUOC()
            {
                MABENHAN = convertBenhanSub_ID(Mabenhan),
                GHICHU = ghichu,
            };
            _db.DONTHUOC.AddObject(newDonThuoc);
            _db.SaveChanges();
        }
        public AddPrescriptionViewModel(BENHNHAN SelectedBenhNhan)
        {
            Benhnhan = SelectedBenhNhan;
            loadBenhan();
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
            insert();
            new MessageBoxCustom("Thông báo", "Thêm đơn thuốc mới thành công cho bệnh nhân: \n"
                + Benhnhan.SUB_ID + ": " + Benhnhan.HOTEN,
                MessageType.Success,
                MessageButtons.OK)
                .ShowDialog();
            Application.Current.MainWindow.Close();
        }

    }
}
