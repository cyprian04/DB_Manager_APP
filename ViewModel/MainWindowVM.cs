using System.Windows.Input;
using System.Windows.Controls;
using GUI_Database_app.Navigation;
using System.Reflection;
using System.Diagnostics;
using System.Windows;

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
        private bool _isToolTipVisible = true;
        public bool IsToolTipVisible
        {
            get => _isToolTipVisible;
            set
            {
                if (_isToolTipVisible != value)
                {
                    _isToolTipVisible = value;
                    OnPropertyChanged(nameof(IsToolTipVisible));
                }
            }
        }

        public MainWindowVM(NavigationService navigationService)
        {
            this.navigationService = navigationService;

            navigationService.Register<View.HomeControl>("Home");
            navigationService.Register<View.ProfileControl>("Profile");
            navigationService.Register<View.DbSettingsControl>("DbSettings");
            navigationService.Register<View.MainInteractionControl>("MainInteraction");

            NavigateTo("Home");
        }

        public ICommand NavigateToCommand => new RelayCommand(param => NavigateTo(param.ToString()));
        public ICommand ResetSessionCommand => new RelayCommand(param => LogOut());
        public ICommand ToggleButtonCommand => new RelayCommand(param => ToggleButtonClick());

        private void NavigateTo(string destination)
        {
            UserControl newControl = navigationService.NavigateTo(destination);
            CurrentControl = (CurrentControl == null || CurrentControl.GetType() != newControl.GetType()) ? newControl : CurrentControl;
        }

        private void LogOut()
        {
            var exePath = Assembly.GetEntryAssembly().Location;
            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true,
                CreateNoWindow = false
            });
            Application.Current.Shutdown();
        }

        private void ToggleButtonClick()
        {
            IsToolTipVisible = !IsToolTipVisible;
        }
    }
}
