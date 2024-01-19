using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.View;
using System.Globalization;

namespace LTTQ_DoAn.ViewModel
{
    public class AddMedicineViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private int id;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public int Id { get => id; set => id = value; }
        public string Tenthuoc
        {
            get => tenthuoc; set
            {
                tenthuoc = value;
                OnPropertyChanged(nameof(Tenthuoc));
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
        public string Donvitinh
        {
            get => donvitinh; set
            {
                donvitinh = value;
                OnPropertyChanged(nameof(Donvitinh));
            }
        }
        public string Giatien
        {
            get => giatien; set
            {
                giatien = value;
                OnPropertyChanged(nameof(Giatien));
            }
        }

        public double Soluong
        {
            get => soluong; set
            {
                soluong = value;
                OnPropertyChanged(nameof(Soluong));
            }
        }

        private string tenthuoc = null;
        private string giatien = null;
        private string ghichu = null;
        private string donvitinh = null;
        private double soluong;



        public AddMedicineViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }
        public void insert()
        {
            THUOC newThuoc = new THUOC()
            {
                TENTHUOC = Tenthuoc,
                GIATIEN = decimal.Parse(Giatien),
                GHICHU= Ghichu,
                DONVITINH = Donvitinh,
                SOLUONG = Soluong,
            };
            _db.THUOC.AddObject(newThuoc);
            _db.SaveChanges();
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
            try
            {
                insert();
                new MessageBoxCustom("Thông báo", "Thêm thuốc mới thành công!",
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close();

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message);
                new MessageBoxCustom("Lỗi", err.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close();
            }
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
    }
}
