using PrekrasnySklep.ViewModels.Tabs;
using System.Windows.Controls;

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
