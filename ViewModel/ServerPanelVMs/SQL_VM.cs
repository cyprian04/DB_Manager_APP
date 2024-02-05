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
        private static CornerRadius adjutedRightCol = new CornerRadius(0, 0, 9, 9);
        private static CornerRadius defaultRightCol = new CornerRadius(0, 0, 9, 0);
        private static Thickness defaultBorder = new Thickness(2, 2, 0, 0);
        private static Thickness adjustedBorder = new Thickness(0, 2, 0, 0);

        private CornerRadius mainAreaRadius = defaultRightCol;
        private Thickness currentThickness = defaultBorder;
        private readonly Data.DBServerContent dBServerContent;
        private FlowDocument _myDocument = new FlowDocument();
        private DataTable querryResult;
        private bool _isVisible = false;

        public CornerRadius MainAreaRadius
        {
            get => mainAreaRadius;
            set
            {
                mainAreaRadius = value;
                OnPropertyChanged(nameof(MainAreaRadius));
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

        public SQL_VM(Data.DBServerContent dBServerContent, ServerPanelVM serverPanelVM)
        {
            this.dBServerContent = dBServerContent;
            serverPanelVM.HideShowBtnChanged += ServerPanelVM_HideShowBtnChanged;

        }

        private void ServerPanelVM_HideShowBtnChanged(object sender, string selectedItem)
        {
            MainAreaRadius = MainAreaRadius != adjutedRightCol ? adjutedRightCol : defaultRightCol;
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
