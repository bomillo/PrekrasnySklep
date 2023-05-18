using PrekrasnySklep.ViewModels.Login;
using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Login
{
    public partial class LoginView : Page
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.PasswordChangedCommand.Execute(passwordBox);
            }
        }
    }
}
