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
    public class ViewMedicineViewModel : BaseViewModel
    {
        private THUOC thuoc = null;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        public THUOC Thuoc { get => thuoc; set => thuoc = value; }
        public ICommand CancelCommand { get; }
        public ViewMedicineViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        public ViewMedicineViewModel(THUOC SelectedThuoc)
        {
            Thuoc = SelectedThuoc;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
    }
}
