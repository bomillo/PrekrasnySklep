using PrekrasnySklep.ViewModels.Tabs;
using System.Windows.Controls;

namespace PrekrasnySklep.Views.Tabs
{
    public partial class OrderDispatcher : Page
    {
        public OrderDispatcher()
        {
            InitializeComponent();
        }

        public OrderDispatcher(TabbedViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
