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
using MySql.Data.MySqlClient;

namespace GUI_Database_app.CustomControls
{
    /// <summary>
    /// Logika interakcji dla klasy DbSettingsControl.xaml
    /// </summary>
    /// 
    public partial class DbSettingsControl : UserControl
    {
        Data.Connection CurrentUserConn = null;

        public DbSettingsControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
            CurrentUserConn = CurrentUserConn_in;
            CurrentUserConn.DisplayAvaliableDatabases(DatabasesListBox);
        }

        private void btn_Connect(object sender, RoutedEventArgs e)
        {
            if (DatabasesListBox.SelectedItem != null)
            {
                // Get the selected option
                CurrentUserConn.ConnectionWithDb((DatabasesListBox.SelectedItem).ToString());
                MessageBox.Show(CurrentUserConn.DbName);
            }
            else           
                MessageBox.Show("Please select database before confirming.");
        }
    }
}
