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
            container.Register<ViewModel.MainInteractionVM>(Lifestyle.Singleton);

            container.Register<View.HomeControl>(Lifestyle.Singleton);          
            container.Register<View.ProfileControl>(Lifestyle.Singleton);
            container.Register<View.DbSettingsControl>(Lifestyle.Singleton);
            container.Register<View.MainInteractionControl>(Lifestyle.Singleton);

            container.Register<Data.Connection>(Lifestyle.Singleton);
            container.Register<Navigation.NavigationService>(Lifestyle.Singleton);
            container.Register<Model.ICurrentUser, Model.CurrentUser>(Lifestyle.Singleton);
          
            container.Register<View.MainInteractionContent.SQLControl>(Lifestyle.Singleton);
            container.Register<View.MainInteractionContent.StructureControl>(Lifestyle.Singleton);
            container.Register<View.MainInteractionContent.RelationsControl>(Lifestyle.Singleton);
            container.GetInstance<View.MainInteractionControl>().DataContext = container.GetInstance<ViewModel.MainInteractionVM>();

            container.Verify();
        }

        private void CreateAndShowLoginForm()
        {
            var loginForm = new View.LoginWindow() { DataContext = container.GetInstance<ViewModel.LoginWindowVM>()};
            loginForm.Show();

            loginForm.IsVisibleChanged += (s, ev) =>
            {
                if (loginForm.IsLoaded && !loginForm.IsVisible)
                {
                    var mainWindow = new View.MainWindow { DataContext = container.GetInstance<ViewModel.MainWindowVM>()};
                    mainWindow.Show();
                }
            };
        }
    }
}
