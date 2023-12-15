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
        public ICommand ChangeServicesCommand { get; }
        public ICommand AddServicesCommand { get; }
        public ICommand DeleteServicesCommand { get; }
        public ServicesViewModel() {
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
