using PrekrasnyDomainLayer.Models;
using PrekrasnySklep.ViewModels.OrderDispatcher;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.OrderDispatcher
{
    public partial class DispatcherListView : UserControl
    {
        public DispatcherListView()
        {
            InitializeComponent();
        }

        private void ListSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView list = listView;
            GridView gView = list.View as GridView;

            var workingWidth = list.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            gView.Columns[0].Width = workingWidth;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            DispatcherListViewModel model = (DispatcherListViewModel)DataContext;
            try
            {
                model.Order = (Order)e.AddedItems[0];
            }
            catch (Exception)
            {
                model.Order = null!;
            }
        }
    }
}
