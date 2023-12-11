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
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            // Konfiguracja dependency injection
            var container = new Container();
            container.Register<Model.ICurrentUser, Model.CurrentUser>();
            container.Verify();

            // Utwórz ViewModel, zależności zostaną automatycznie wstrzyknięte
            var loginViewModel = container.GetInstance<ViewModel.LoginFormVM>();
            var mainViewModel = container.GetInstance<MainViewModel>();

            // Przekazuj ViewModeli referencje do kontenera
            loginViewModel.Container = container;
            mainViewModel.Container = container;

            var loginForm = new LoginForm();
            loginForm.IsVisibleChanged += (s, ev) =>
              {
                  if (!loginForm.IsVisible && loginForm.IsLoaded)
                  {
                      var mainWindow = new MainWindow { DataContext = loginViewModel };
                      mainWindow.Show();
                  }
              };
        }
    }
}
