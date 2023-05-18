using PrekrasnySklep.ViewModels.Forms;
using System.Windows;

namespace PrekrasnySklep.Views.Forms
{
    public partial class RemoveCategory : Window
    {
        public RemoveCategory(RemoveCategoryModel viewModel)
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
