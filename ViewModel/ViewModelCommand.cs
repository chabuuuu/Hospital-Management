using System;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    internal class ViewModelCommand : ICommand
    {
        private object executeloginCommand;
        private object value;

        public ViewModelCommand(Action<object> value)
        {
            this.value = value;
        }

        public ViewModelCommand(object executeloginCommand, object value)
        {
            this.executeloginCommand = executeloginCommand;
            this.value = value;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}