using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class EditUserModel : ViewModelBase
    {
        public User user { get; set; }
        public RelayCommand EditUserCommand { get; }
        public EditUserModel(User userToEdit)
        {
            user = userToEdit;
            EditUserCommand = new RelayCommand(EditUser, CanExecute);
        }
        public void EditUser(object sender)
        {
        }
        private bool CanExecute(object sender)
        {
            return !string.IsNullOrWhiteSpace(user.UserName) && !user.UserRole.Equals("");
        }
    }
}