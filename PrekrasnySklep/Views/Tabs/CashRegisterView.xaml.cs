using PrekrasnySklep.ViewModels.Tabs;
using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Tabs
{
    public partial class CashRegister : Page
    {
        public CashRegister()
        {
            InitializeComponent();
        }

        public CashRegister(TabbedViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CartSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = cartList;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.60;
            var col2 = 0.20;
            var col3 = 0.20;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }
    }
}
