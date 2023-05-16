using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Forms;
using PrekrasnySklep.ViewModels.Login;
using PrekrasnySklep.Views.Forms;
using PrekrasnySklep.Views.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Tabs;

public class UserManagerViewModel : TabbedViewModel
{   
    private readonly UserService _userService;
    private ObservableCollection<User>  users;
    private User? selectedUser;
    public RelayCommand AddUserCommand { get; }
    public RelayCommand RemoveUserCommand { get; }
    public RelayCommand EditUserCommand { get; }
    public UserManagerViewModel() : base(title: "Users Manager") { 
        _userService = new UserService();
        AddUserCommand = new RelayCommand(AddUser);
        RemoveUserCommand = new RelayCommand(RemoveUser,CanRemove);//CanRemove
        EditUserCommand = new RelayCommand(EditUser,CanEdit);//CanEdit
        SelectedUser = null!;
        Users = new ObservableCollection<User>(AppState.SharedContext.Users.ToList());
    }
    public User SelectedUser
    {
        get => selectedUser!;
        set
        {
            selectedUser = value;
            EditUserCommand.RaiseCanExecuteChanged();
            RemoveUserCommand.RaiseCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    public ObservableCollection<User> Users
    {
        get => users;
        set
        {
            users = value;
            SelectedUser = null!;
            EditUserCommand.RaiseCanExecuteChanged();
            RemoveUserCommand.RaiseCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    private void AddUser(object parameter)
    {
        var window = new RegisterView(new Login.RegisterViewModel(_userService));
        window.Owner = Application.Current.MainWindow;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
        Users = new ObservableCollection<User>(AppState.SharedContext.Users.ToList());
        OnPropertyChanged();
    }
    private void EditUser(object parameter)
    {
        EditUserModel model = new EditUserModel(SelectedUser);
        Window window = new EditUser(model);
        window.DataContext= model;
        window.ShowDialog();
        Users = new ObservableCollection<User>(AppState.SharedContext.Users.ToList());
        OnPropertyChanged();

    }
    private void RemoveUser(object parameter)
    {
        RemoveUserModel model = new RemoveUserModel(SelectedUser);
        Window window = new RemoveUser(model);
        window.DataContext = model;
        window.ShowDialog();
        Users = new ObservableCollection<User>(AppState.SharedContext.Users.ToList());
        OnPropertyChanged();
    }
    private bool CanEdit(object parameter)
    {
        return selectedUser is not null;
    }
    private bool CanRemove(object parameter)
    {
        if (selectedUser is not null && selectedUser.UserRole != UserRole.Admin)
            return true;
        else
            return false;
    }

    public override void Sync()
    {

    }
}
