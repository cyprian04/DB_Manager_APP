using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_Database_app.ViewModel.ServerPanelVMs
{
    class StructureVM : ViewModelBase
    {
        private static ImageBrush show = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-show.png", UriKind.RelativeOrAbsolute)));
        private static ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));
        private static CornerRadius adjutedRightCol = new CornerRadius(0, 0, 9, 0);
        private static CornerRadius defaultRightCol = new CornerRadius(9, 0, 9, 0);
        private static GridLength defaultLeftColWidth = new GridLength(200);
        private static GridLength adjustedLeftColWidth = new GridLength(0);
        private static Thickness defaultBorder = new Thickness(2, 2, 0, 0);
        private static Thickness adjustedBorder = new Thickness(0, 2, 0, 0);        
        
        private ImageBrush hideShowBtn = show;
        private CornerRadius currentRadius = defaultRightCol;
        private GridLength currentContentColWidth = defaultLeftColWidth;
        private Thickness currentThickness = defaultBorder;

        private readonly Data.DBServerContent dbServerContent;
        private string currentContent = null;
        private ObservableCollection<string> tableListBox = new ObservableCollection<string>();
        private DataTable querryResult;
        private bool _isVisible = false;
        private string _selectedItem;
        private string currentTB = "Not choosen";

        public ImageBrush HideShowBtn
        {
            get => hideShowBtn;
            set
            {
                hideShowBtn = value;
                OnPropertyChanged(nameof(HideShowBtn));
            }
        }
        public CornerRadius CurrentRadius
        {
            get => currentRadius;
            set
            {
                currentRadius = value;
                OnPropertyChanged(nameof(CurrentRadius));
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
        public Thickness CurrentThickness
        {
            get => currentThickness;
            set
            {
                currentThickness = value;
                OnPropertyChanged(nameof(CurrentThickness));
            }
        }

        public ObservableCollection<string> TableListBox
        {
            get { return tableListBox; }
            set
            {
                if (tableListBox != value)
                {
                    tableListBox = value;
                    OnPropertyChanged(nameof(TableListBox));
                }
            }
        }
        public DataTable QuerryResult
        {
            get { return querryResult; }
            set
            {
                querryResult = value;
                OnPropertyChanged(nameof(QuerryResult));
            }
        }
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
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value == null)
                {
                    _selectedItem = value;
                    CurrentTB = "Not choosen";
                }
                else if (_selectedItem != value && value != null)
                {
                    _selectedItem = value;
                    CurrentTB = _selectedItem;
                }                    
                if(QuerryResult != null) QuerryResult.Clear();
                OnPropertyChanged(nameof(SelectedItem));
                currentContent = null;
                IsVisible = false;
            }
        }
        public string CurrentTB
        {
            get { return currentTB; }
            set
            {
                if (currentTB != value)
                {
                    currentTB = value;
                    OnPropertyChanged(nameof(CurrentTB));
                }
            }
        }

        public StructureVM(Data.DBServerContent dbServerContent, ServerPanelVM serverPanelVM)
        {
            this.dbServerContent = dbServerContent;
            dbServerContent.collectionTables = TableListBox;

            serverPanelVM.SelectedItemChanged += ServerPanelVM_SelectedItemChanged;
        }

        private void ServerPanelVM_SelectedItemChanged(object sender, string selectedItem)
        {
            if (selectedItem != null) dbServerContent.DisplayCurrentListBox("TABLES", tableListBox);
            else tableListBox.Clear();
        }

        public ICommand ShowStructCommand => new RelayCommand(param => ShowTable(param.ToString()));
        public ICommand ShowDataCommand => new RelayCommand(param => ShowTable(param.ToString()));
        public ICommand ShowHideCommand => new RelayCommand(ShowHide);

        private void ShowTable(string content)
        {
            if (currentContent != content && CurrentTB != "Not choosen")
            {
                QuerryResult = dbServerContent.ExecuteAndCheckSQLQuerry(content + currentTB + ";");
                IsVisible = QuerryResult.Rows.Count == 0 ? false : true;
                currentContent = content;
            }
        }

        private void ShowHide(object parameter)
        {
            HideShowBtn = HideShowBtn != hide ? hide : show;
            CurrentContentColWidth = CurrentContentColWidth != adjustedLeftColWidth ? adjustedLeftColWidth : defaultLeftColWidth;
        }
    }
}
