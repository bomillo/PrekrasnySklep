using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views;
using PrekrasnySklep.Views.Login;
using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrekrasnySklep.ViewModels.Login;

public class RegisterViewModel : ViewModelBase
{
    private readonly UserService _userService;
    private string _username;
    private string _password;
    private string _passwordValidation;
    private string _errorMessage;

    public RelayCommand RegisterCommand { get; }
    public RelayCommand PasswordChangedCommand { get; }
    public RelayCommand PasswordValidationChangedCommand { get; }
    public RelayCommand GoBackCommand { get; }

    public RegisterViewModel(UserService userService)
    {
        _userService = userService;
        RegisterCommand = new RelayCommand(Register, CanRegister);
        PasswordChangedCommand = new RelayCommand(OnPasswordsChanged);
        PasswordValidationChangedCommand = new RelayCommand(OnPasswordValidationChanged);

        GoBackCommand = new RelayCommand(GoBack);
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
            RegisterCommand.RaiseCanExecuteChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            RegisterCommand.RaiseCanExecuteChanged();
        }
    }

    public string PasswordValidation
    {
        get => _passwordValidation;
        set
        {
            _passwordValidation = value;
            OnPropertyChanged();
            ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
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


    private void OnPasswordsChanged(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            Password = passwordBox.Password;
        }
    }

    private void OnPasswordValidationChanged(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            PasswordValidation = passwordBox.Password;
        }
    }

    private bool CanRegister(object parameter)
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            ErrorMessage = "Username must not be empty!";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Password must not be empty!";
            return false;
        }

        if (string.IsNullOrWhiteSpace(PasswordValidation))
        {
            ErrorMessage = "Please repeat password!";
            return false;
        }

        if (Password != PasswordValidation)
        {
            ErrorMessage = "Passwords don't match!";
            return false;
        }

        ErrorMessage = "";
        return true;
    }

    private void Register(object parameter)
    {
        var success = _userService.RegisterNewUserAndLogin(Username, Password);

        if (success)
        {
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = new EmptyWindow();
            Application.Current.MainWindow.Show();
        }
        else
        {
            ErrorMessage = "Username is already taken!";
        }
    }

    private void GoBack(object parameter) {
        LoginViewModel loginViewModel = new LoginViewModel(new UserService());
        LoginView registerView = new LoginView(loginViewModel);
        ((LoginViewFrame)Application.Current.MainWindow).frame.Navigate(registerView);
    }
}

