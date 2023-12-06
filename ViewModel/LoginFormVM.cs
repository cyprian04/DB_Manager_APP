using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_Database_app.ViewModel
{
    public class LoginFormVM : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _host;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                OnPropertyChanged(nameof(Host));
            }
        }



        //private void btn_LogIn(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtUser.Text) || !string.IsNullOrEmpty(txtPassword.Password) || !string.IsNullOrEmpty(txtHost.Text))
        //    {
        //        connection.Initialize(txtHost.Text, txtUser.Text, txtPassword.Password);
        //
        //        if (connection.VerifyCredentials())
        //        {
        //            MainWindow mainWindow = new MainWindow(connection);
        //            mainWindow.Show();
        //            LoginMediaVideo.Stop();
        //            this.Close();
        //        }
        //        else
        //        {
        //            txtUser.Text = "";
        //            txtPassword.Password = "";
        //            txtHost.Text = "";
        //        }
        //    }
        //}
        //
        //private void LoginMediaVideo_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    if (LoginMediaVideo.Source != null)
        //    {
        //        LoginMediaVideo.Position = TimeSpan.Zero; // Reset the position to the beginning
        //        LoginMediaVideo.Play(); // Start playing again
        //    }
        //}
    }
}
