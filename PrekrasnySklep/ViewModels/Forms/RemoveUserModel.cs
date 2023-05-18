using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System.Linq;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class RemoveUserModel : ViewModelBase
    {
        public User user { get; set; }
        public RelayCommand RemoveUserCommand { get; }
        public RemoveUserModel(User userToRemove)
        {
            user = userToRemove;
            RemoveUserCommand = new RelayCommand(DeleteUser);
        }
        public void DeleteUser(object sender)
        {
            var result = AppState.SharedContext.Users.Remove(user);
            AppState.SharedContext.SaveChanges();
            if (result is not null)
            {
                Application.Current.Windows.OfType<RemoveUser>().FirstOrDefault()!.Close();
            }
        }
    }
}
