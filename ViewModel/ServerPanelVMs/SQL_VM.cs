using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GUI_Database_app.ViewModel.ServerPanelVMs
{
    class SQL_VM : ViewModelBase
    {
        private readonly Data.DBServerContent dBServerContent;
        private FlowDocument _myDocument = new FlowDocument();

        public FlowDocument MyDocument
        {
            get { return _myDocument; }
            set { _myDocument = value; }
        }

        public SQL_VM(Data.DBServerContent dBServerContent)
        {
            this.dBServerContent = dBServerContent;
        }

        public ICommand ExecuteQuerryCommand => new RelayCommand(ExecuteQuerry);

        private void ExecuteQuerry(object parameter)
        {
            string queryText = GetPlainTextFromFlowDocument(MyDocument);
            // if (!string.IsNullOrEmpty(QuerryText)) dBServerContent.ExecuteAndCheckSQLQuerry();
        }

        private string GetPlainTextFromFlowDocument(FlowDocument flowDocument)
        {
            TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            return textRange.Text;
        }
    }
}
