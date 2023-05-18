using PrekrasnySklep.ViewModels.Forms;
using System.Windows;

namespace PrekrasnySklep.Views.Forms
{
    public partial class Order : Window
    {
        public Order(OrderModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
