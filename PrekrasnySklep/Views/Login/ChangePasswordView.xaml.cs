using PrekrasnySklep.ViewModels.Login;
using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Login
{
    public partial class ChangePasswordView : Window
    {
        public ChangePasswordView(ChangePasswordViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void OldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.OldPassword = passwordBox.Password;
            }
        }

        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.NewPassword = passwordBox.Password;
            }
        }

        private void RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.RepeatPassword = passwordBox.Password;
            }
        }
    }
}
