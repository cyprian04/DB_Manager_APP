using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GUI_Database_app.Model;

namespace GUI_Database_app.ViewModel
{
    class LoginWindowVM : ViewModelBase
    {
        private string username;
        private SecureString password;
        private string host;
        private bool isVisible = true;

        private readonly ICurrentUser currentUser;
        private readonly Data.Connection conn;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Host
        {
            get => host;
            set
            {
                host = value;
                OnPropertyChanged(nameof(Host));
            }
        }
       
        public ICommand LoginCommand{ get; }

        public LoginWindowVM(ICurrentUser currentUser, Data.Connection conn)
        {
            this.currentUser = currentUser;
            this.conn = conn;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {  
            if (Host != null && Username !=null && conn.VerifyCredentials(Host, Username, Password))
            {
                currentUser.Username = Username;
                currentUser.Password = Password;
                currentUser.Host = Host;
                IsVisible = false;
            }
            else
            {
                MessageBox.Show("DENIED");
                Username = null;
                Host = null;
                Password = null;
            }
        }
    }
}
