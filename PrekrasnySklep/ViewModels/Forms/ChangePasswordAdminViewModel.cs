using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class ChangePasswordAdminViewModel : ViewModelBase
    {
        private string _newPassword;
        private string _repeatPassword;
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

        private readonly UserService _userService;
        public RelayCommand ChangePasswordCommand { get; }
        private User user;

        public ChangePasswordAdminViewModel(User selectedUser)
        {
            user = selectedUser;
            _userService = new UserService();
            ChangePasswordCommand = new RelayCommand(ChangePassword, CanExecute);
        }
        private void ChangePassword(object sender)
        {
            _userService.ModifyUser(user,newPassword: _newPassword);
            Application.Current.Windows.OfType<ChangePasswordAdminView>().FirstOrDefault()!.Close();
        }

        private bool CanExecute(object sender)
        {
            return  !string.IsNullOrWhiteSpace(_newPassword) && _newPassword.Equals(_repeatPassword);
        }
    }
}
