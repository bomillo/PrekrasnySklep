using PrekrasnySklep.ViewModels.Login;
using PrekrasnySklep.ViewModels.Tabs;
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

namespace PrekrasnySklep.Views.Tabs
{
    public partial class OrderManager : Page
    {
        public OrderManager()
        {
            InitializeComponent();
        }

        public OrderManager(TabbedViewModel viewModel)
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

        private void CartSizeChanged2(object sender, SizeChangedEventArgs e)
        {
            ListView listView = listView2;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.80;
            var col2 = 0.20;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;

        }

        private void cartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            OrderManagerViewModel model = (OrderManagerViewModel)DataContext;
            try
            {
                model.SelectedOrder = ((DisplayOrder)e.AddedItems[0]).Order;
            }
            catch (Exception)
            {
                model.SelectedOrder = null!;
            }
        }
    }
}
