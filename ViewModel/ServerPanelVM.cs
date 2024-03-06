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
using Microsoft.Win32;

namespace GUI_Database_app.ViewModel
{
    class ServerPanelVM : ViewModelBase
    {
        private static ImageBrush show = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-show.png", UriKind.RelativeOrAbsolute)));
        private static ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));
        private static GridLength defaultLeftColWidth = new GridLength(200);
        private static GridLength adjustedLeftColWidth = new GridLength(0);

        private ImageBrush hideShowBtn = show;
        private GridLength currentContentColWidth = defaultLeftColWidth;
        private ObservableCollection<string> databasesListBox = new ObservableCollection<string>();
        private string selectedItem;
        public event EventHandler<string> SelectedItemChanged;
        public event EventHandler<string> HideShowBtnChanged;
        private string currentDB = "Not choosen";

        private readonly NavigationService navigationService;
        private readonly Data.DBServerContent dbServerContent;
        private UserControl currentControl;

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
            get => currentControl;
            set
            {
                currentControl = value;
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
            get { return selectedItem; }
            set
            {
                if(value == null && !dbServerContent.GetVerifyConnectionDB(CurrentDB))
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    CurrentDB = "Not choosen";
                }
                else if (selectedItem != value && value != null)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    dbServerContent.ChoosenDB(selectedItem);
                    CurrentDB = selectedItem;
                }
                SelectedItemChanged?.Invoke(this, selectedItem);
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

            NavigateTo("SQL");
        }

        public void InitializeDatabases()
        {
            dbServerContent.DisplayCurrentListBox("Databases", databasesListBox);
        }

        public ICommand NavigateToCommand => new RelayCommand(param => NavigateTo(param.ToString()));
        public ICommand ShowHideCommand => new RelayCommand(ShowHide);
        public ICommand ImportDBCommand => new RelayCommand(ImportDB);
        public ICommand ExportDBCommand => new RelayCommand(ExportDB);


        private void ShowHide(object parameter)
        {
            HideShowBtn = HideShowBtn != hide ? hide : show;
            HideShowBtnChanged?.Invoke(this, HideShowBtn.ToString());
            CurrentContentColWidth = CurrentContentColWidth != adjustedLeftColWidth ? adjustedLeftColWidth : defaultLeftColWidth; 
        }

        private void NavigateTo(string destination)
        {
            UserControl newControl = navigationService.NavigateTo(destination);
            CurrentControl = (CurrentControl == null || CurrentControl.GetType() != newControl.GetType()) ? newControl : CurrentControl;
        }

        private void ImportDB(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File",
                Filter = "All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show("Selected file: " + openFileDialog.FileName);
                dbServerContent.ImportDB(openFileDialog.FileName);
            }
            else
                MessageBox.Show("Abortet action");
        }

        private void ExportDB(object parameter)
        {
            if (SelectedItem != null) dbServerContent.ExportDB(SelectedItem); 
            else MessageBox.Show("No database selected");
        }

    }
}
