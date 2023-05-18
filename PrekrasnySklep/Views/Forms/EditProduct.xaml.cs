using PrekrasnySklep.ViewModels.Forms;
using System.Windows;

namespace PrekrasnySklep.Views.Forms
{
    public partial class EditProduct : Window
    {
        public EditProduct(EditProductModel viewModel)
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
