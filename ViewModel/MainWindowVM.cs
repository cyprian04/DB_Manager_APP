using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GUI_Database_app.Model;
using SimpleInjector;

namespace GUI_Database_app.ViewModel
{
    class MainWindowVM
    {
        private readonly ICurrentUser _currentUser;
        public Container Container { get; set; }

        public MainWindowVM(ICurrentUser currentUser)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }
    }
}
