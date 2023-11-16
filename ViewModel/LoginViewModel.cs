using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private object executeloginCommand;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

            }
        }
        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));

            }
        }
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommnad { get; }
        public ICommand ShowPasswordCommnad { get; }
        public ICommand RememberPassword { get;  }
        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteloginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommnad = new ViewModelCommand(p => ExecuteRecoverPassCommand("",""));
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length == 0 || Password == null || Password.Length == 0) 
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
        }
        private void ExecuteloginCommand(object obj)
        {
            throw new NotSupportedException();
        }
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotSupportedException();
        }
    }
}
