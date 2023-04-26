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
    public partial class TabView : Page
    {
        public TabView()
        {
            InitializeComponent();
        }

        public TabView(TabbedViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
