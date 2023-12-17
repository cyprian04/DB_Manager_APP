using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI_Database_app
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // Data.Connection CurrentUserConn = null;
       // CustomControls.ViewControl View = null;
       // CustomControls.MainInteractionControl MainInteraction = null;
       // CustomControls.DbSettingsControl DbSettings = null;
       // CustomControls.HomeControl Home = new CustomControls.HomeControl();

        public MainWindow()
        {
            InitializeComponent();
        }

        //public MainWindow(Data.Connection CurrentUserConn_in)
        //{
        //    InitializeComponent();
        //    ContentArea.Content = Home;
        //    CurrentUserConn = CurrentUserConn_in;
        //
        //    View = new CustomControls.ViewControl(CurrentUserConn);
        //    MainInteraction = new CustomControls.MainInteractionControl(CurrentUserConn);
        //    DbSettings = new CustomControls.DbSettingsControl(CurrentUserConn);
        //}

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
