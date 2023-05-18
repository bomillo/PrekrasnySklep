using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Login
{
    public partial class LoginViewFrame : Window
    {
        public LoginViewFrame(Page newWindow)
        {
            InitializeComponent();
            frame.Navigate(newWindow);
        }
    }
}
