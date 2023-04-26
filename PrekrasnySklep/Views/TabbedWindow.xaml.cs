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

namespace PrekrasnySklep.Views
{
    public partial class TabbedWindow : Window
    {
        public TabbedWindow(TabbedWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void HandleDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TabControl_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void TabControl_Selected(object sender, SelectionChangedEventArgs e)
        {
            TabbedWindowViewModel? vm = this.DataContext as TabbedWindowViewModel;
            vm?.SetDataContext(sender, e);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            TabbedWindowViewModel? vm = this.DataContext as TabbedWindowViewModel;
            vm?.GetDataContext(sender, e);
        }
    }
}
