using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_Database_app.View.MainInteractionContent
{
    public partial class SQLControl : UserControl
    {
        Data.Connection CurrentUserConn = null;
        string querryText = null;

        public SQLControl()
        {
            InitializeComponent();
        }

        private void btn_ExecuteSqlCommand(object sender, RoutedEventArgs e)
        {
            // setting string value by getting first and last pointer in the textRange from our SqlCommandTextBox (RichTextBox)
            querryText = new TextRange(SqlCommandTextBox.Document.ContentStart, SqlCommandTextBox.Document.ContentEnd).Text;
            //CurrentUserConn.ExecuteAndCheckSQLQuerry(Data.Connection.TypeOfQuerry.defaultQuerry, querryText, QuerryrResultDataGrid, TextQuerryResultInfo, BorderQuerryResultInfo);
        }
    }
}
