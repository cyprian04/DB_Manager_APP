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

        public MainWindow(Data.Connection  CurrentUserConn_in)
        {
            InitializeComponent();

            User = new Data.CurrentUser(CurrentUserConn_in);
            ContentArea.Content = new CustomControls.HomeControl();
            MessageBox.Show(User.Username);
        }

        private void IsCurrentContentAreaSame(UserControl control) 
        {
            if (ContentArea.Content.GetType() != control.GetType())
            {
                ContentArea.Content = null;
                ContentArea.Content = control;
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
            IsCurrentContentAreaSame(new CustomControls.HomeControl());
        }

        private void btn_view(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(new CustomControls.ViewControl());
        }

        private void btn_Sql(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(new CustomControls.SqlCommandControl());
        }

        private void btn_settings(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(new CustomControls.DbSettingsControl());
        }

        private void btn_LogOut(object sender, RoutedEventArgs e)
        {
            LoginForm LoginWindow = new LoginForm();
            LoginWindow.Show();
            this.Close();
        }

    }
}
