using System.Windows.Input;
using System.Windows.Controls;
using GUI_Database_app.Navigation;
using GUI_Database_app.CustomControls;
using System;

namespace GUI_Database_app.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private readonly NavigationService navigationService;

        private UserControl _currentControl;
        public UserControl CurrentControl
        {
            get => _currentControl;
            set
            {
                _currentControl = value;
                OnPropertyChanged(nameof(CurrentControl));
            }
        }

        public MainWindowVM(NavigationService navigationService)
        {
            this.navigationService = navigationService;

            navigationService.Register("Home", () => new HomeControl());
            navigationService.Register("DbSettings", () => new DbSettingsControl());

            NavigateTo("Home");
        }

        public ICommand NavigateToCommand => new RelayCommand(param => NavigateTo(param.ToString()));

        private void NavigateTo(string destination)
        {
            CurrentControl = navigationService.NavigateTo(destination);
        }

    }
}
