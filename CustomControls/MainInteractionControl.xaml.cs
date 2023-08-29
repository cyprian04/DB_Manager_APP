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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_Database_app.CustomControls
{
    /// <summary>
    /// Logika interakcji dla klasy SqlCommandControl.xaml
    /// </summary>
    public partial class MainInteractionControl : UserControl
    {
        Data.Connection CurrentUserConn = null;

        public MainInteractionControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
            CurrentUserConn = CurrentUserConn_in;
            CurrentUserConn.DisplayAvaliableDatabases(DatabasesListBox);
        }

        private void DatabasesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatabasesListBox.SelectedItem != null)
            {
                // Get the selected option
                CurrentUserConn.ConnectionWithDb(DatabasesListBox.SelectedItem.ToString());

                //for testing
                MessageBox.Show(CurrentUserConn.DbName);
            }
        }
    }
}
