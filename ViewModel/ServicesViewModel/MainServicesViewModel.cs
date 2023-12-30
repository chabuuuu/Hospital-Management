﻿using LTTQ_DoAn.View;
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

            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteAddCommand(object? obj) {
            return true;
        }
        private void ExecuteAddCommand(object? obj) {
            AddService wd = new AddService();

            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteDeleteCommand(object? obj) {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj) {

        }

    }
}
