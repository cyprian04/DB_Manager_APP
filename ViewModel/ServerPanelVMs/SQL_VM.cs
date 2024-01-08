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
                dBServerContent.DisplayCurrentListBox(dBServerContent.collection, "Databases");
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
