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
        public MainWindow()
        {
            InitializeComponent();
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

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tip_home.Visibility = Visibility.Collapsed;
                tip_contacts.Visibility = Visibility.Collapsed;
                tip_settings.Visibility = Visibility.Collapsed;
                tip_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tip_home.Visibility = Visibility.Visible;
                tip_contacts.Visibility = Visibility.Visible;
                tip_settings.Visibility = Visibility.Visible;
                tip_signout.Visibility = Visibility.Visible;
            }
        }
    }
}
