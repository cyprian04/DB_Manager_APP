using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GUI_Database_app.Navigation;

namespace GUI_Database_app.ViewModel
{
    class ServerPanelVM : ViewModelBase
    {
        static ImageBrush show = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-show.png", UriKind.RelativeOrAbsolute)));
        static ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));
        static GridLength defaultLeftColWidth = new GridLength(200);
        static GridLength adjustedLeftColWidth = new GridLength(0);

        private ObservableCollection<string> databasesListBox = new ObservableCollection<string>();
        private string _selectedItem;
        private string currentDB = "Not choosen";
        private ImageBrush hideShowBtn = show;
        private GridLength currentContentColWidth = defaultLeftColWidth;

        private readonly NavigationService navigationService;
        private readonly Data.DBServerContent dbServerContent;
        private UserControl _currentControl;

        public ImageBrush HideShowBtn
        {
            get => hideShowBtn;
            set
            {
                hideShowBtn = value;
                OnPropertyChanged(nameof(HideShowBtn));
            }
        }
        public GridLength CurrentContentColWidth
        {
            get => currentContentColWidth;
            set
            {
                currentContentColWidth = value;
                OnPropertyChanged(nameof(CurrentContentColWidth));
            }
        }
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
                    dbServerContent.ChoosenDB(_selectedItem);
                    CurrentDB = _selectedItem;
                }
            }
        }
        public string CurrentDB
        {
            get { return currentDB; }
            set
            {
                if (currentDB != value)
                {
                    currentDB = value;
                    OnPropertyChanged(nameof(CurrentDB));
                }
            }
        }

        public ServerPanelVM(NavigationService navigationService, Data.DBServerContent dbServerContent)
        {
            this.navigationService = navigationService;
            this.dbServerContent = dbServerContent;

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
        public ICommand ShowHideCommand => new RelayCommand(ShowHide);

        private void ShowHide(object parameter)
        {
            HideShowBtn = HideShowBtn != hide ? hide : show;
            CurrentContentColWidth = CurrentContentColWidth != adjustedLeftColWidth ? adjustedLeftColWidth : defaultLeftColWidth; 
        }

        private void NavigateTo(string destination)
        {
            UserControl newControl = navigationService.NavigateTo(destination);
            CurrentControl = (CurrentControl == null || CurrentControl.GetType() != newControl.GetType()) ? newControl : CurrentControl;
        }

    }
}
