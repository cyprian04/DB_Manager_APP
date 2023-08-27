using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;


namespace GUI_Database_app
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data.CurrentUser User = null;
        CustomControls.ViewControl View = null;
        CustomControls.SqlCommandControl SqlCommand = null;
        CustomControls.DbSettingsControl DbSettings = null;
        CustomControls.HomeControl Home = new CustomControls.HomeControl();

        public MainWindow(Data.Connection  CurrentUserConn_in)
        {
            InitializeComponent();
            ContentArea.Content = Home;

            User = new Data.CurrentUser(CurrentUserConn_in);
            View = new CustomControls.ViewControl(CurrentUserConn_in);
            SqlCommand = new CustomControls.SqlCommandControl(CurrentUserConn_in);
            DbSettings = new CustomControls.DbSettingsControl(CurrentUserConn_in);
            
        }

        private void IsCurrentContentAreaSame(UserControl ChosenControl) 
        {
            if (ContentArea.Content.GetType() != ChosenControl.GetType())
            {
                ContentArea.Content = null;
                ContentArea.Content = ChosenControl;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btn_Minimalize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_home(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(Home);
        }

        private void btn_view(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(View);
        }

        private void btn_Sql(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(SqlCommand);
        }

        private void btn_settings(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(DbSettings);
        }

        private void btn_LogOut(object sender, RoutedEventArgs e)
        {
            LoginForm LoginWindow = new LoginForm();
            LoginWindow.Show();
            this.Close();
        }

    }
}
