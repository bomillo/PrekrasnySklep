using PrekrasnySklep.Base;

namespace PrekrasnySklep.ViewModels.Tabs;

public abstract class TabbedViewModel : ViewModelBase
{
    public string TabTitle { get; init; }

    public virtual void Sync() { OnPropertyChanged(); }

    public TabbedViewModel(string title = "")
    {
        TabTitle = title;
    }
}

