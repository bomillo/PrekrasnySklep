using PrekrasnyDomainLayer.Models;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.OrderDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Tabs;

public class OrderDispatcherViewModel : TabbedViewModel
{
    public OrderDispatcherViewModel() : base(title: "Orders Dispatcher") {
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
