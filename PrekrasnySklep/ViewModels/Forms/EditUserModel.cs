using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using PrekrasnySklep.Views.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class EditUserModel : ViewModelBase
    {
        private string _userName;
        private UserRole? _userRole;
        private readonly CollectionView _roles;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public UserRole? UserRole
        {
            get { return _userRole; }
            set
            {
                if (_userRole == value) return;
                _userRole = value;
                OnPropertyChanged();
            }
        }
        public User user { get; set; }
        public RelayCommand EditUserCommand { get; }
        public RelayCommand ChangePasswordCommand { get; }
        public EditUserModel(User userToEdit)
        {
            user = userToEdit;
            EditUserCommand = new RelayCommand(EditUser, CanExecute);
            ChangePasswordCommand = new RelayCommand(ChangePassword);

            IList<UserRole> list = new List<UserRole>()
            {
                PrekrasnyDomainLayer.Models.Enums.UserRole.Admin,
                PrekrasnyDomainLayer.Models.Enums.UserRole.CustomerService,
                PrekrasnyDomainLayer.Models.Enums.UserRole.Shipping
            };
            _roles = new CollectionView(list);
        }
        public CollectionView RoleChoose
        {
            get { return _roles; }
        }
        public void EditUser(object sender)
        {
            user.UserRole = _userRole??user.UserRole;
            var result = AppState.SharedContext.Users.Update(user);
            AppState.SharedContext.SaveChanges();
            if (result is not null)
            {
                Application.Current.Windows.OfType<EditUser>().FirstOrDefault()!.Close();
            }
        }
        public void ChangePassword(object sender)
        {
            ChangePasswordAdminViewModel model = new ChangePasswordAdminViewModel(user);
            Window window = new ChangePasswordAdminView(model);
            window.DataContext = model;
            window.ShowDialog();

        }
        private bool CanExecute(object sender)
        {
            return !string.IsNullOrWhiteSpace(user.UserName) ;
        }
    }
}