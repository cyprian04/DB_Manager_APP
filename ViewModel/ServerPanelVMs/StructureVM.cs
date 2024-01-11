using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_Database_app.ViewModel.ServerPanelVMs
{
    class StructureVM : ViewModelBase
    {
        private readonly Data.DBServerContent dbServerContent;
        private string currentContent = null;
        private ObservableCollection<string> tableListBox = new ObservableCollection<string>();
        private DataTable querryResult;
        private bool _isVisible = false;
        private string _selectedItem;
        private string currentTB = "Not choosen";

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

        private void ShowTable(string content)
        {
            if (currentContent != content && CurrentTB != "Not choosen")
            {
                QuerryResult = dbServerContent.ExecuteAndCheckSQLQuerry(content + currentTB + ";");
                IsVisible = QuerryResult.Rows.Count == 0 ? false : true;
                currentContent = content;
            }
        }
    }
}
