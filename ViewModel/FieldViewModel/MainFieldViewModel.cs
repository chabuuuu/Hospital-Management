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
    public class FieldViewModel : BaseViewModel
    {
        public ICommand ViewFieldCommand { get; }
        public ICommand ChangeFieldCommand { get; }
        public ICommand AddFieldCommand { get; }
        public ICommand DeleteFieldCommand { get; }
        public FieldViewModel() {
            ViewFieldCommand = new ViewModelCommand(ExecuteViewCommand, CanExecuteViewCommand);
            ChangeFieldCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddFieldCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteFieldCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }

        private bool CanExecuteViewCommand(object? obj) {
            return true; //ko điều kiện
        }
        private void ExecuteViewCommand(object? obj) {
            ViewField wd = new ViewField();
            
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteChangeCommand(object? obj) {
            return false; //ko điều kiện
        }
        private void ExecuteChangeCommand(object? obj) {
            ChangeField wd = new ChangeField();

            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteAddCommand(object? obj) {
            return false;//ko điều kiện
        }
        private void ExecuteAddCommand(object? obj) {
            AddField wd = new AddField();

            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private bool CanExecuteDeleteCommand(object? obj) {
            return true;//ko điều kiện
        }
        private void ExecuteDeleteCommand(object? obj) {
            
        }

    }
}
