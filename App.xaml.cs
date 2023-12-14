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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new Container();
            container.Register<ViewModel.LoginFormVM>();
            container.Register<Model.ICurrentUser, Model.CurrentUser>();
            container.Verify();

            var currentUser = new Model.CurrentUser();
            var loginViewModel = new ViewModel.LoginFormVM(currentUser) ;

            // Przekazuj ViewModeli referencje do kontenera
            loginViewModel.Container = container;
            // var mainViewModel = container.GetInstance<ViewModel.MainWindowVM>();
            // mainViewModel.Container = container;

            var loginForm = new LoginForm() {DataContext = loginViewModel };
            loginForm.Visibility = Visibility.Visible;
            loginForm.Show();
            loginForm.IsVisibleChanged += (s, ev) =>
            {
                if (loginForm.IsLoaded && !loginForm.IsVisible)
                {
                    var mainWindow = new MainWindow { DataContext = loginViewModel };
                    mainWindow.Show();
                }
            };
        }
    }
}
