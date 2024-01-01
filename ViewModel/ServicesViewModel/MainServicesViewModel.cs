using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class ServicesViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db;

        private List<DICHVU> dichvu;
        private DICHVU selecteddichvu;
        public ICommand ChangeServicesCommand { get; }
        public ICommand AddServicesCommand { get; }
        public ICommand DeleteServicesCommand { get; }
        public List<DICHVU> Dichvu
        {
            get => dichvu; set
            {
                dichvu = value;
                OnPropertyChanged(nameof(Dichvu));
            }
        }
        public DICHVU Selecteddichvu
        {
            get => selecteddichvu; set
            {
                selecteddichvu = value;
                OnPropertyChanged(nameof(Selecteddichvu));
            }
        }
        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
            Dichvu = _db.DICHVU.ToList();
        }
        public ServicesViewModel() {
            Load();
            ChangeServicesCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddServicesCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteServicesCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }
        private bool CanExecuteChangeCommand(object? obj) {
            return true;
        }
        private void ExecuteChangeCommand(object? obj) {
            ChangeService wd = new ChangeService();
            wd.Closed += ChangeService_Closed;

            if (Selecteddichvu != null)
            {
                wd.DataContext = new ChangeServicesViewModel(Selecteddichvu);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }

        }

        private void ChangeService_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteAddCommand(object? obj) {
            return true;
        }
        private void ExecuteAddCommand(object? obj) {
            AddService wd = new AddService();
            wd.Closed += AddService_Closed;
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void AddService_Closed(object sender, EventArgs e)
        {
            Load();
        }

        private bool CanExecuteDeleteCommand(object? obj) {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj) {
            try
            {
                int Id = Selecteddichvu.MADICHVU;
                var deleteMember = _db.DICHVU.Where(m => m.MADICHVU == Id).Single();
                _db.DICHVU.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa khoa: \nMã dịch vụ: " +
                        Selecteddichvu.SUB_ID.ToString() + "\nTên dịch vụ: " +
                        Selecteddichvu.TENDICHVU,
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                Load();
            }
            catch (Exception e)
            {
                new MessageBoxCustom(
                    "Thông báo",
                    e.Message + "\nLỗi: " + e.GetType().ToString(),
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }

    }
}
