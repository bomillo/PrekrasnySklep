using PrekrasnySklep.ViewModels.Tabs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Tabs
{
    public partial class OrderViewer : Page
    {
        public OrderViewer()
        {
            InitializeComponent();
        }

        public OrderViewer(TabbedViewModel viewModel)
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
            OrderViewerViewModel model = (OrderViewerViewModel)DataContext;
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
