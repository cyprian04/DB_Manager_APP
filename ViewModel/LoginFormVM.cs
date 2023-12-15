using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GUI_Database_app.Model;


namespace GUI_Database_app.ViewModel
{
    class LoginFormVM : ViewModelBase
    {
        private string _username;
        private string _password;
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
       
        public ICommand LoginCommand{ get; }

        public LoginFormVM(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            
            if (Username == "Cyprian")
            {
                _currentUser.Username = "CYPU";
                MessageBox.Show("ACCEPTED");
                IsVisible = false;
            }
            else
            {
                MessageBox.Show("DENIED");
                Username = string.Empty;
                Password = string.Empty;
                Host = string.Empty;
            }
        }
    }
}
