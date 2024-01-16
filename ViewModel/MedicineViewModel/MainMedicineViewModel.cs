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
        public MedicineViewModel()
        {
            Load();
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ViewCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
            Load();
        }
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj)
        {
            Load();
        }

        private bool CanExecuteViewCommand(object? obj)
        {
            return true;
        }
        private void ExecuteViewCommand(object? obj)
        {

        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeCommand(object? obj)
        {

        }
    }
}
