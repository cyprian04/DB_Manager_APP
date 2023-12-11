using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI_Database_app.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data.Connection CurrentUserConn = null;
        CustomControls.ViewControl View = null;
        CustomControls.MainInteractionControl MainInteraction = null;
        CustomControls.DbSettingsControl DbSettings = null;
        CustomControls.HomeControl Home = new CustomControls.HomeControl();

        public MainWindow()
        {
            InitializeComponent();
            ContentArea.Content = Home;
        }

        public MainWindow(Data.Connection CurrentUserConn_in)
        {
            
            CurrentUserConn = CurrentUserConn_in;

            View = new CustomControls.ViewControl(CurrentUserConn);
            MainInteraction = new CustomControls.MainInteractionControl(CurrentUserConn);
            DbSettings = new CustomControls.DbSettingsControl(CurrentUserConn);
        }

        private void IsCurrentContentAreaSame(UserControl ChosenControl)
        {
            if (ContentArea.Content.GetType() != ChosenControl.GetType())
                ContentArea.Content = ChosenControl;

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btn_Minimalize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_WindowMinMax(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
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

        private void btn_Main(object sender, RoutedEventArgs e)
        {
            //for testing
            //string temp = CurrentUserConn.DbName +" " + CurrentUserConn.ServerIp + " " + CurrentUserConn.Username + " " + CurrentUserConn.Password;
            //MessageBox.Show(temp);
            IsCurrentContentAreaSame(MainInteraction);
        }

        private void btn_settings(object sender, RoutedEventArgs e)
        {
            IsCurrentContentAreaSame(DbSettings);
        }

        private void btn_LogOut(object sender, RoutedEventArgs e)
        {
            LoginForm LoginWindow = new LoginForm();
            LoginWindow.Show();
            CurrentUserConn.DisconnectUserFromServer();
            CurrentUserConn = null;
            Home = null;
            View = null;
            MainInteraction = null;
            DbSettings = null;
            this.Close();
        }

        private void Btn_MouseMove(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            var pos = e.GetPosition(button);
            var centerX = button.ActualWidth /2;
            var centerY = button.ActualHeight /2;

            var distance = Math.Sqrt(Math.Pow(pos.X - centerX, 2) + Math.Pow(pos.Y - centerY, 2)); 

            if(distance <= 20)
            {
                button.Height = 45;
                button.Width = 45;
            }
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            button.Height= 50;
            button.Width = 50;
        }
    }
}
