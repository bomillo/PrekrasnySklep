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
    public RelayCommand AboutAppCommand { get; }
    public RelayCommand ChangePasswordCommand { get; }

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
    private bool _onInitFullScreen;
    public bool OnInitFullScreen
    {
        get => _onInitFullScreen;
        set
        {
            if(value !=  _onInitFullScreen)
            {
                _onInitFullScreen = value;
                ChangeFullScreenSetting();
            }
            
        }
    }
    public TabbedWindowViewModel()
    {
        
        _userService = new UserService();
        AboutAppCommand = new RelayCommand(AboutApp);
        LogoutCommand = new RelayCommand(LogOut);
        ChangePasswordCommand = new RelayCommand(ChangePassword);
        OnInitFullScreen = AppState.CurrentUser!.OnInitFullScreen;
        
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
        if(e.AddedItems.Count > 0 && e.AddedItems[0] is TabbedViewModel)
        {
            currentTabbedViewModel = e.AddedItems[0] as TabbedViewModel;
        }
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
    private void ChangeFullScreenSetting()
    {
        AppState.CurrentUser.OnInitFullScreen = _onInitFullScreen;
        _userService.ModifyUser(AppState.CurrentUser);
        Application.Current.MainWindow.WindowState = _onInitFullScreen ? WindowState.Maximized : WindowState.Normal;
    }
    private void AboutApp(object sender)
    {
        var window = new AboutWindow();
        window.Height = 300;
        window.Width = 400;
        window.Owner = Application.Current.MainWindow;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
    }
    private void ChangePassword(object sender)
    {
        var window = new ChangePasswordView(new Login.ChangePasswordViewModel());
        window.Height = 300;
        window.Width = 400;
        window.Owner = Application.Current.MainWindow;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
    }
}

