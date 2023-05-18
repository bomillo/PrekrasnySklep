using PrekrasnyDomainLayer.Models;
using PrekrasnySklep.ViewModels.OrderDispatcher;
using PrekrasnySklep.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrekrasnySklep.Views.OrderDispatcher
{
    public partial class DispatcherListView : UserControl
    {
        public DispatcherListView()
        {
            InitializeComponent();
        }

        private void CartSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView list = listView;
            GridView gView = list.View as GridView;

            var workingWidth = list.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            gView.Columns[0].Width = workingWidth;
        }

        private void cartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
