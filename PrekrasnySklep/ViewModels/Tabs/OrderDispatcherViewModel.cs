using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.OrderDispatcher;

namespace PrekrasnySklep.ViewModels.Tabs;

public class OrderDispatcherViewModel : TabbedViewModel
{
    public OrderDispatcherViewModel() : base(title: "Orders Dispatcher")
    {
        CurrentPanel = new DispatcherListViewModel(this);
    }

    private ViewModelBase currentPanel;

    public ViewModelBase CurrentPanel
    {
        get => currentPanel;
        set
        {
            currentPanel = value;
            OnPropertyChanged();
        }
    }

    public override void Sync()
    {
        base.Sync();
        CurrentPanel = new DispatcherListViewModel(this);
    }
}
