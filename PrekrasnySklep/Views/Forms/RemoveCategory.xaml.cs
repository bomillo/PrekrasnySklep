using PrekrasnySklep.ViewModels.Forms;
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
    /// Logika interakcji dla klasy RemoveCategory.xaml
    /// </summary>
    public partial class RemoveCategory : Window
    {
        public RemoveCategory(RemoveCategoryModel viewModel)
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
