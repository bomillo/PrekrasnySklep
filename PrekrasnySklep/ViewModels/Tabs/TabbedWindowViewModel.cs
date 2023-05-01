using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views;
using PrekrasnySklep.Views.Login;
using PrekrasnySklep.Views.Tabs;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;

namespace PrekrasnySklep.ViewModels.Tabs;


public class TabbedWindowViewModel : ViewModelBase
{
    private ObservableCollection<TabbedViewModel>? _pageViews;
    private readonly UserService _userService;

    private List<(UserRoleAccessRule userRole, Func<TabbedViewModel> vmFactory)> availableViewModels = new List<(UserRoleAccessRule, Func<TabbedViewModel>)>
    {
        (UserRoleAccessRule.CustomerService,    () => new CashRegisterViewModel()),
        (UserRoleAccessRule.Any,                () => new OrderViewerViewModel()),
        (UserRoleAccessRule.Any,                () => new InventoryViewerViewModel()),
        (UserRoleAccessRule.Shipping,           () => new OrderDispatcherViewModel()),
        (UserRoleAccessRule.Shipping,           () => new InventoryManagerViewModel()),
        (UserRoleAccessRule.Admin,              () => new OrderManagerViewModel()),
        (UserRoleAccessRule.Admin,              () => new UserManagerViewModel()),
    };

    TabbedViewModel? currentTabbedViewModel;

    public RelayCommand LogoutCommand { get; }

    private bool _darkThemeChecked;
    public bool DarkThemeChecked 
    { 
        get => _darkThemeChecked;
        set
        {
            _darkThemeChecked = value;
            if (_darkThemeChecked)
            {
                ChangeTheme(UserTheme.Dark);
            }
            else
            {
                ChangeTheme(UserTheme.Light);
            }
            OnPropertyChanged();
        } 
    }
    public TabbedWindowViewModel()
    {
        _userService = new UserService();
        LogoutCommand = new RelayCommand(LogOut);
        ((App)Application.Current).ChangeTheme(AppState.CurrentUser.Theme);
        DarkThemeChecked = AppState.CurrentUser.Theme == UserTheme.Dark;
        foreach (var viewModelEntry in availableViewModels) 
        {
            if (((int)viewModelEntry.userRole & (int)AppState.CurrentUser!.UserRole) != 0) 
            {
                PageViews.Add(viewModelEntry.vmFactory.Invoke());
            }
        }
    }

    public ObservableCollection<TabbedViewModel> PageViews
    {
        get
        {
            if (_pageViews == null)
            {
                _pageViews = new ObservableCollection<TabbedViewModel>();
            }
            return _pageViews;
        }
    }

    public void LogOut(object sender) {
        new UserService().Logout();
        var currWindow = Application.Current.MainWindow;
        ((App)Application.Current).StartApp();
        currWindow.Close();
    }

    public void SetDataContext(object sender, SelectionChangedEventArgs e) {
        currentTabbedViewModel = e.AddedItems[0] as TabbedViewModel;
    }

    internal void GetDataContext(object sender, RoutedEventArgs e)
    {
        ((Page)sender).DataContext = currentTabbedViewModel;
        currentTabbedViewModel.Sync();
    }

    private void ChangeTheme(UserTheme theme)
    {
        AppState.CurrentUser!.Theme = theme;
        _userService.ModifyUser(AppState.CurrentUser);
        ((App)Application.Current).ChangeTheme(theme);
    }
}

