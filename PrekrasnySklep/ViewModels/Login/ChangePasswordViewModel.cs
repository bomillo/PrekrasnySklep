using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Login;
using System.Linq;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Login
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private string _oldPassword;
        private string _newPassword;
        private string _repeatPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged();
            }
        }
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }
        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                OnPropertyChanged();
            }
        }

        private string _errorMessage;
        private readonly UserService _userService;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand ChangePasswordCommand { get; }

        public ChangePasswordViewModel()
        {
            _userService = new UserService();
            ChangePasswordCommand = new RelayCommand(ChangePassword, CanExecute);
        }

        private void ChangePassword(object sender)
        {
            if (_userService.ValidateOldPassword(_oldPassword))
            {
                _userService.ModifyCurrentUser(newPassword: _newPassword);
                Application.Current.Windows.OfType<ChangePasswordView>().FirstOrDefault()!.Close();
            }
            else
            {
                ErrorMessage = "Old password didn't match";
                OnPropertyChanged();
            }
        }

        private bool CanExecute(object sender)
        {
            return !string.IsNullOrWhiteSpace(_oldPassword) && !string.IsNullOrWhiteSpace(_newPassword)
                   && _newPassword.Equals(_repeatPassword) && !_oldPassword.Equals(_newPassword);
        }

    }
}
