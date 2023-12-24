using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GUI_Database_app.Navigation;

namespace GUI_Database_app.ViewModel
{
    class ServerPanelVM : ViewModelBase
    {
        private ObservableCollection<string> databasesListBox = new ObservableCollection<string>();
        private string _selectedItem;
        private readonly NavigationService navigationService;
        private readonly Data.DBServerContent dbServerContent;
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

        public ObservableCollection<string> DatabasesListBox
        {
            get { return databasesListBox; }
            set
            {
                if (databasesListBox != value)
                {
                    databasesListBox = value;
                    OnPropertyChanged(nameof(DatabasesListBox));
                }
            }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public ServerPanelVM(NavigationService navigationService, Data.DBServerContent dBServerContent)
        {
            this.navigationService = navigationService;
            this.dbServerContent = dBServerContent;

            navigationService.Register<View.ServerPanelContent.SQLControl>("SQL");
            navigationService.Register<View.ServerPanelContent.StructureControl>("Struct");
            navigationService.Register<View.ServerPanelContent.RelationsControl>("Relations");

            NavigateTo("SQL");
        }

        public void InitializeDatabases()
        {
            dbServerContent.DisplayCurrentListBox(databasesListBox, "Databases");
        }

        public ICommand NavigateToCommand => new RelayCommand(param => NavigateTo(param.ToString()));

        private void NavigateTo(string destination)
        {
            UserControl newControl = navigationService.NavigateTo(destination);
            CurrentControl = (CurrentControl == null || CurrentControl.GetType() != newControl.GetType()) ? newControl : CurrentControl;
        }
    }
}
