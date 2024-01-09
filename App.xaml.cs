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
            container.Register<ViewModel.LoginWindowVM>(Lifestyle.Singleton);
            container.Register<ViewModel.MainWindowVM>(Lifestyle.Singleton);
            container.Register<ViewModel.ServerPanelVM>(Lifestyle.Singleton);

            container.Register<View.HomeControl>(Lifestyle.Singleton);          
            container.Register<View.ProfileControl>(Lifestyle.Singleton);
            container.Register<View.DbSettingsControl>(Lifestyle.Singleton);
            container.Register<View.ServerPanelControl>(Lifestyle.Singleton);

            container.Register<Data.Connection>(Lifestyle.Singleton);
            container.Register<Data.DBServerContent>(Lifestyle.Singleton);
            container.Register<Navigation.NavigationService>(Lifestyle.Singleton);
            container.Register<Model.ICurrentUser, Model.CurrentUser>(Lifestyle.Singleton);

            container.Register<ViewModel.ServerPanelVMs.SQL_VM>(Lifestyle.Singleton);
            container.Register<ViewModel.ServerPanelVMs.StructureVM>(Lifestyle.Singleton);

            container.Register<View.ServerPanelContent.SQLControl>(Lifestyle.Singleton);
            container.Register<View.ServerPanelContent.StructureControl>(Lifestyle.Singleton);
            container.Register<View.ServerPanelContent.RelationsControl>(Lifestyle.Singleton);
            container.GetInstance<View.ServerPanelControl>().DataContext = container.GetInstance<ViewModel.ServerPanelVM>();
            container.GetInstance<View.ServerPanelContent.SQLControl>().DataContext = container.GetInstance<ViewModel.ServerPanelVMs.SQL_VM>();
            container.GetInstance<View.ServerPanelContent.StructureControl>().DataContext = container.GetInstance<ViewModel.ServerPanelVMs.StructureVM>();


            container.Verify();
        }

        private void CreateAndShowLoginForm()
        {
            var loginForm = new View.LoginWindow() { DataContext = container.GetInstance<ViewModel.LoginWindowVM>()};
            loginForm.Show();

            loginForm.IsVisibleChanged += (s, ev) =>
            {
                if (loginForm.IsLoaded && !loginForm.IsVisible && !string.IsNullOrEmpty(container.GetInstance<Data.Connection>().MySqlConn.ConnectionString))
                {
                    var mainWindow = new View.MainWindow { DataContext = container.GetInstance<ViewModel.MainWindowVM>()};
                    container.GetInstance<ViewModel.ServerPanelVM>().InitializeDatabases();
                    mainWindow.Show();
                }
            };
        }
    }
}
