using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_Database_app
{
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            loginForm.IsVisibleChanged += (s, ev) =>
              {
                  if (!loginForm.IsVisible && loginForm.IsLoaded)
                  {
                      var mainWindow = new MainWindow();
                      mainWindow.Show();
                      loginForm.Close();
                  }
              };
        }
    }
}
