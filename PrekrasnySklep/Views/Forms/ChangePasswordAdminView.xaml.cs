using PrekrasnySklep.ViewModels.Forms;
using PrekrasnySklep.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrekrasnySklep.Views.Forms
{
    /// <summary>
    /// Logika interakcji dla klasy ChangePasswordAdminView.xaml
    /// </summary>
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
