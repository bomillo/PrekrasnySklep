using PrekrasnySklep.ViewModels.Forms;
using System.Windows;

namespace PrekrasnySklep.Views.Forms
{
    public partial class AddProduct : Window
    {
        public AddProduct(AddProductModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
