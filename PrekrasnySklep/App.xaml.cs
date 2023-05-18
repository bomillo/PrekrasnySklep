﻿using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.ViewModels.Login;
using PrekrasnySklep.Views.Login;
using System;
using System.Windows;

namespace PrekrasnySklep
{
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StartApp();
        }

        public void StartApp()
        {
            PrekrasnyContext.InitalizeSharedContext();
            var _userService = new UserService();

            LoginViewModel loginViewModel = new LoginViewModel(new UserService());

            LoginView loginView = new LoginView(loginViewModel);

            MainWindow = new LoginViewFrame(loginView);
            MainWindow.Show();
        }

        public void ChangeTheme(UserTheme theme)
        {
            Resources.Clear();
            Resources.MergedDictionaries.Clear();
            if (theme == UserTheme.Light)
            {
                SetDictionaries("Light.xaml");
            }
            else
            {
                SetDictionaries("Dark.xaml");
            }
        }

        private void SetDictionaries(string dictionaryFile)
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"Assets/Themes/{dictionaryFile}", UriKind.Relative) });
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"Assets/Themes/Styles.xaml", UriKind.Relative) });
        }
    }
}
