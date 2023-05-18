using PrekrasnySklep.ViewModels.Login;
using System.Windows;
using System.Windows.Controls;


namespace PrekrasnySklep.Views.Login
{
    public partial class RegisterView : Window
    {
        public RegisterView(RegisterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.PasswordChangedCommand.Execute(passwordBox);
            }
        }

        private void PasswordBox_PasswordValidationChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.PasswordValidationChangedCommand.Execute(passwordBox);
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
