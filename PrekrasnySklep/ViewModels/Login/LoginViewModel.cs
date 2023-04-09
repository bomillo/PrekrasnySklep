using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views;
using PrekrasnySklep.Views.Login;
using System;
using System.Security;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PrekrasnySklep.ViewModels.Login;

public class LoginViewModel : ViewModelBase
{
    private readonly UserService _userService;
    private string _username;
    private string _password;
    private string _errorMessage;

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand PasswordChangedCommand { get; }

    public LoginViewModel(UserService userService)
    {
        _userService = userService;
        LoginCommand = new RelayCommand(Login, CanLogin);
        RegisterCommand = new RelayCommand(Register);
        PasswordChangedCommand = new RelayCommand(OnPasswordChanged);
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }


    private void OnPasswordChanged(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            Password = passwordBox.Password;
        }
    }

    private bool CanLogin(object parameter)
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private void Login(object parameter)
    {
        try
        {
            bool success = _userService.Login(Username, Password);
            if (success)
            {
                ErrorMessage = string.Empty;
                
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = new EmptyWindow();
                Application.Current.MainWindow.Show();
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
    }

    private void Register(object parameter)
    {
        RegisterViewModel loginViewModel = new RegisterViewModel(new UserService());
        RegisterView registerView = new RegisterView(loginViewModel);
        ((LoginViewFrame)Application.Current.MainWindow).frame.Navigate(registerView);
    }
}

