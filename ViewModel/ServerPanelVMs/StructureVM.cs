using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<string> tableListBox;
        private string _selectedItem;
        private string currentTb;

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
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value == null && !dbServerContent.GetVerifyConnectionDB(CurrentTb))
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    CurrentTb = "Not choosen";
                }
                else if (_selectedItem != value && value != null)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    dbServerContent.ChoosenDB(_selectedItem);
                    CurrentTb = _selectedItem;
                }
            }
        }
        public string CurrentTb
        {
            get { return currentTb; }
            set
            {
                if (currentTb != value)
                {
                    currentTb = value;
                    OnPropertyChanged(nameof(CurrentTb));
                }
            }
        }

        public StructureVM(Data.DBServerContent dbServerContent)
        {
            this.dbServerContent = dbServerContent;
        }

        public ICommand ShowStructCommand => new RelayCommand(param => ShowTable(param.ToString()));
        public ICommand ShowDataCommand => new RelayCommand(param => ShowTable(param.ToString()));

        private void ShowTable(string content)
        {
            if (currentContent != content)
            {
                dbServerContent.ExecuteAndCheckSQLQuerry(content);
                currentContent = content;
            }
        }
    }
}
