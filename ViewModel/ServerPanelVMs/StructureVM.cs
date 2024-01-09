﻿using System;
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
        private ObservableCollection<string> tableListBox = new ObservableCollection<string>();
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
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value == null && !dbServerContent.GetVerifyConnectionDB(CurrentTB))
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    CurrentTB = "Not choosen";
                }
                else if (_selectedItem != value && value != null)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    dbServerContent.ChoosenDB(_selectedItem);
                    CurrentTB = _selectedItem;
                }
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
            dbServerContent.DisplayCurrentListBox("TABLES",tableListBox);
        }

        public ICommand ShowStructCommand => new RelayCommand(param => ShowTable(param.ToString()));
        public ICommand ShowDataCommand => new RelayCommand(param => ShowTable(param.ToString()));

        private void ShowTable(string content)
        {
            if (currentContent != content)
            {
                dbServerContent.ExecuteAndCheckSQLQuerry(content + currentTB+";");
                currentContent = content;
            }
        }
    }
}
