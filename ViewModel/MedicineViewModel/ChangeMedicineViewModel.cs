using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.View;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeMedicineViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public THUOC Thuoc
        {
            get => thuoc; set
            {
                thuoc = value;
                OnPropertyChanged(nameof(Thuoc));
            }
        }

        public List<string> Unit { get => unit; set => unit = value; }
        public string Donvi
        {
            get => donvi; set
            {
                donvi = value;
                OnPropertyChanged(nameof(Donvi));
            }
        }

        private THUOC thuoc = null;
        private List<String> unit = new List<string>() { "Viên", "Hộp", "Gói", "Lọ", "Vỉ" };
        private string donvi;
        private void update()
        {
            THUOC updateThuoc = (from m in _db.THUOC
                                       where m.MATHUOC == Thuoc.MATHUOC
                                       select m).Single();
            updateThuoc.TENTHUOC = updateThuoc.TENTHUOC;
            updateThuoc.GIATIEN = updateThuoc.GIATIEN;
            updateThuoc.GHICHU = updateThuoc.GHICHU;
            updateThuoc.DONVITINH = updateThuoc.DONVITINH;
            _db.SaveChanges();
        }
        public ChangeMedicineViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        public ChangeMedicineViewModel(THUOC SelectedThuoc)
        {
            Thuoc = SelectedThuoc;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        private void ExecuteConfirmChangeCommand(object? obj)
        {
            try
            {
                update();
                new MessageBoxCustom("Thông báo", "Sửa thông tin thuốc thành công!",
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close();

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close();
            }
        }
        private bool CanExecuteConfirmChangeCommand(object? obj)
        {
            return true;
        }
    }
}
