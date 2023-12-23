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
        Data.Connection conn = new Data.Connection();

        private string _username;
        private SecureString _password;
        private string _host;
        private bool _isVisible = true;
        private readonly ICurrentUser _currentUser;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }
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
       
        public ICommand LoginCommand{ get; }

        public LoginWindowVM(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {  
            if (conn.VerifyCredentials(Host, Username, Password))
            {
                _currentUser.Username = Username;
                _currentUser.Password = Password;
                _currentUser.Host = Host;
                MessageBox.Show("ACCEPTED");
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
