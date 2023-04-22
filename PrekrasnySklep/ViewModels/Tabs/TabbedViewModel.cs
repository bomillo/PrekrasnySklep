using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views;
using PrekrasnySklep.Views.Login;
using PrekrasnySklep.Views.Tabs;
using System;
using System.Collections.ObjectModel;
using System.Security;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

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

