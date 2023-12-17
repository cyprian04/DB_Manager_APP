using System.Windows.Input;
using System.Windows.Controls;
using GUI_Database_app.Navigation;

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

            navigationService.Register<View.Home.HomeControl>("Home");
            navigationService.Register<View.DbSettings.DbSettingsControl>("DbSettings");

            NavigateTo("Home");
        }

        public ICommand NavigateToCommand => new RelayCommand(param => NavigateTo(param.ToString()));

        private void NavigateTo(string destination)
        {
            UserControl newControl = navigationService.NavigateTo(destination);

            if (CurrentControl == null || CurrentControl.GetType() != newControl.GetType())
                CurrentControl = newControl;
        }
    }
}
