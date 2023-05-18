using PrekrasnySklep.ViewModels.Forms;
using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Forms
{
    public partial class ChangePasswordAdminView : Window
    {
        public ChangePasswordAdminView(ChangePasswordAdminViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordAdminViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.NewPassword = passwordBox.Password;
            }
        }

        private void RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordAdminViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.RepeatPassword = passwordBox.Password;
            }
        }
    }
}
