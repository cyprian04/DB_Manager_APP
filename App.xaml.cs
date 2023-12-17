using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SimpleInjector;


namespace GUI_Database_app
{
    public partial class App : Application
    {
        private Container container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetupContainer();
            CreateAndShowLoginForm();
        }

        private void SetupContainer()
        {
            container = new Container();
            container.Register<ViewModel.LoginFormVM>(Lifestyle.Singleton);
            container.Register<ViewModel.MainWindowVM>(Lifestyle.Singleton);
            container.Register<Model.ICurrentUser, Model.CurrentUser>(Lifestyle.Singleton);
            container.Register<Navigation.NavigationService>(Lifestyle.Singleton);
            container.Register<View.Home.HomeControl>(Lifestyle.Singleton);
            container.Register<View.DbSettings.DbSettingsControl>(Lifestyle.Singleton);
            container.Verify();
        }

        private void CreateAndShowLoginForm()
        {
            var navigation = container.GetInstance<Navigation.NavigationService>();
            var currentUser = container.GetInstance<Model.ICurrentUser>();
            var loginViewModel = container.GetInstance<ViewModel.LoginFormVM>();
            var mainViewModel = container.GetInstance<ViewModel.MainWindowVM>();

            var loginForm = new LoginForm() { DataContext = loginViewModel };
            loginForm.Show();

            loginForm.IsVisibleChanged += (s, ev) =>
            {
                if (loginForm.IsLoaded && !loginForm.IsVisible)
                {
                    MessageBox.Show(currentUser.Username);
                    var mainWindow = new MainWindow { DataContext = mainViewModel };
                    mainWindow.Show();
                }
            };
        }
    }
}
