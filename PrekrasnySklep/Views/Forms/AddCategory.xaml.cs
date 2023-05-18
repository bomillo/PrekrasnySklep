using PrekrasnySklep.ViewModels.Forms;
using System.Windows;

namespace PrekrasnySklep.Views.Forms
{
    public partial class AddCategory : Window
    {
        public AddCategory(AddCategoryModel viewModel)
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
