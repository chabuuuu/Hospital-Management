using LTTQ_DoAn.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class MedicineViewModel : BaseViewModel
    {
        public ICommand ViewCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public bool deleteVisibility = true;
        public bool changeVisibility = true;
        public bool addVisibility = true;
        private List<THUOC> medicine;
        private THUOC selectedItem = null;
        QUANLYBENHVIENEntities _db;

        public List<THUOC> Medicine
        {
            get => medicine; set
            {
                medicine = value;
                OnPropertyChanged(nameof(Medicine));
            }
        }
        public THUOC SelectedItem
        {
            get => selectedItem; set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private void Load()
        {
            _db = new QUANLYBENHVIENEntities();
            Medicine = _db.THUOC.ToList();
        }
        public bool DeleteVisibility
        {
            get => deleteVisibility; set
            {
                deleteVisibility = value;
                OnPropertyChanged(nameof(DeleteVisibility));
            }
        }
        public bool AddVisibility
        {
            get => addVisibility; set
            {
                addVisibility = value;
                OnPropertyChanged(nameof(AddVisibility));
            }
        }
        public bool ChangeVisibility
        {
            get => changeVisibility; set
            {
                changeVisibility = value;
                OnPropertyChanged(nameof(ChangeVisibility));
            }
        }
        public MedicineViewModel()
        {
            Load();
            Set_permission(MainViewModel._currentUserAccount.LOAITAIKHOAN);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }
        void Set_permission(string type)
        {
            switch (type)
            {
                case "Admin":
                    Set_admin();
                    break;
                case "Staff":
                    Set_staff();
                    break;
                case "Doctor":
                    Set_doctor();
                    break;
                default:
                    MessageBox.Show("nothing");
                    break;
            }
        }
        void Set_doctor()
        {
            deleteVisibility = false;
            changeVisibility = false;
            addVisibility = false;
        }
        void Set_admin()
        {
            deleteVisibility = true;
            changeVisibility = true;
            addVisibility = true;
        }
        void Set_staff()
        {
            deleteVisibility = false;
            changeVisibility = false;
            addVisibility = false;
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddMedicine wd = new AddMedicine();
            wd.Closed += AddMedicine_Closed;
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private void AddMedicine_Closed(object sender, EventArgs e)
        {
            Load();
        }
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj)
        {
            try
            {
                int Id = SelectedItem.MATHUOC;
                var deleteMember = _db.THUOC.Where(m => m.MATHUOC == Id).Single();
                _db.THUOC.DeleteObject(deleteMember);
                _db.SaveChanges();
                new MessageBoxCustom("Thành công", "Đã xóa thuốc: " +
                    SelectedItem.SUB_ID.ToString(),
                    MessageType.Success, MessageButtons.OK).ShowDialog();
                Load();
            }
            catch (Exception e)
            {
                new MessageBoxCustom("Lỗi", "Không thể xóa", MessageType.Error, MessageButtons.OKCancel).ShowDialog();
            }
        }

        private bool CanExecuteViewCommand(object? obj)
        {
            return true;
        }
        private void ExecuteViewCommand(object? obj)
        {
            ViewMedicine wd = new ViewMedicine();
            if (SelectedItem != null)
            {
                wd.DataContext = new ViewMedicineViewModel(SelectedItem);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeCommand(object? obj)
        {
            ChangeMedicine wd = new ChangeMedicine();
            wd.Closed += ChangeMedicine_Closed;
            if (SelectedItem != null)
            {
                wd.DataContext = new ChangeMedicineViewModel(SelectedItem);
                Application.Current.MainWindow = wd;
                wd.ShowDialog();
            }
        }
        private void ChangeMedicine_Closed(object sender, EventArgs e)
        {
            Load();
        }
    }
}
