using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace GUI_Database_app.ViewModel.ServerPanelVMs
{
    class SQL_VM : ViewModelBase
    {
        private readonly Data.DBServerContent dBServerContent;
        private FlowDocument _myDocument = new FlowDocument();
        private DataTable querryResult;
        private bool _isVisible = false;

        public FlowDocument MyDocument
        {
            get { return _myDocument; }
            set { _myDocument = value; }
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

        public SQL_VM(Data.DBServerContent dBServerContent)
        {
            this.dBServerContent = dBServerContent;
        }

        public ICommand ExecuteQuerryCommand => new RelayCommand(ExecuteQuerry);

        private void ExecuteQuerry(object parameter)
        {
            string queryText = GetPlainTextFromFlowDocument(MyDocument);
            if (!string.IsNullOrEmpty(queryText))
            {
                QuerryResult = dBServerContent.ExecuteAndCheckSQLQuerry(queryText);
                IsVisible = QuerryResult.Rows.Count == 0 ? false : true;
                dBServerContent.DisplayCurrentListBox("Databases", dBServerContent.collectionDB);
            }
            else 
                MessageBox.Show("Type in SQL querry first!");
        }

        private string GetPlainTextFromFlowDocument(FlowDocument flowDocument)
        {
            TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            return textRange.Text;
        }
    }
}
