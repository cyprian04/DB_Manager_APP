using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI_Database_app.Navigation;

namespace GUI_Database_app.ViewModel
{
    class MainInteractionVM
    {
        private readonly NavigationService navigationService;

        public MainInteractionVM(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
