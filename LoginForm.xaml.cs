﻿using System;
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
using System.Windows.Shapes;

namespace GUI_Database_app
{
    /// <summary>
    /// Logika interakcji dla klasy LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
            LoginMediaVideo.Play();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btn_Minimalize(object sender, RoutedEventArgs e)
        {
           WindowState = WindowState.Minimized;
        }

        private void btn_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
        }

        private void btn_LogIn(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "admin" && txtPassword.Password == "1234")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                LoginMediaVideo.Stop();
                this.Close();
            }
            else
            {
                txtUser.Text = "";
                txtPassword.Password = "";
            }
        }

        private void LoginMediaVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (LoginMediaVideo.Source != null)
            {
                LoginMediaVideo.Position = TimeSpan.Zero; // Reset the position to the beginning
                LoginMediaVideo.Play(); // Start playing again
            }
        }
    }
}
