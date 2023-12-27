using LTTQ_DoAn.Model;
using LTTQ_DoAn.View;
using LTTQ_DoAn.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;

namespace LTTQ_DoAn.ViewModel
{
    public class HealthRecordViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand ExitCommand { get; }
        public BENHNHAN Benhnhan
        {
            get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        public List<BENHAN> Listbenhan { get => listbenhan; set
            {
                listbenhan = value;
                OnPropertyChanged(nameof(Listbenhan));
            }
        }

        private BENHNHAN benhnhan;
        private List<BENHAN> listbenhan;
        private void findBenhAn()
        {

            List<BENHAN> query = (from m in _db.BENHAN
                                  where m.MABENHNHAN == Benhnhan.MABENHNHAN
                                  select m).ToList();
            Listbenhan = query;
        }
        public HealthRecordViewModel()
        {
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
        }
        public HealthRecordViewModel(BENHNHAN SelectedBenhNhan)
        {
            Benhnhan = SelectedBenhNhan;
            findBenhAn();
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
        }

        private bool CanExecuteExitCommand(object obj)
        {
            return true;
        }

        private void ExecuteExitCommand(object obj)
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddHealthRecord wd = new AddHealthRecord();

            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj)
        {

        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeCommand(object? obj)
        {
            ChangeHealthRecord wd = new ChangeHealthRecord();

            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
    }
}
