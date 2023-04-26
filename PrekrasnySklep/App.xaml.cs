using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.ViewModels.Login;
using PrekrasnySklep.Views.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrekrasnySklep
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StartApp();
        }

        public void StartApp() {
            PrekrasnyContext.InitalizeSharedContext();

            LoginViewModel loginViewModel = new LoginViewModel(new UserService());

            LoginView loginView = new LoginView(loginViewModel);

            MainWindow = new LoginViewFrame(loginView);
            MainWindow.Show();
        }
    }
}
