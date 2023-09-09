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
        MainInteraction.MainContentArea.StructureControl  Structure = null;
        MainInteraction.MainContentArea.AuthorizedControl Authorized = null;
        MainInteraction.MainContentArea.RelationsControl Relations = null;
        MainInteraction.MainContentArea.SQLControl SQL = null;

        public MainInteractionControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
            CurrentUserConn = CurrentUserConn_in;
            CurrentUserConn.DisplayAvaliableDatabases(DatabasesListBox);

            SQL = new MainInteraction.MainContentArea.SQLControl(CurrentUserConn);
            Structure = new MainInteraction.MainContentArea.StructureControl(CurrentUserConn);
            Authorized = new MainInteraction.MainContentArea.AuthorizedControl(CurrentUserConn);
            Relations = new MainInteraction.MainContentArea.RelationsControl(CurrentUserConn);

            MainInteractionContentArea.Content = SQL;
        }

        private void DatabasesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatabasesListBox.SelectedItem != null)
            {
                // Get the selected option

                CurrentUserConn.ConnectionWithDb(DatabasesListBox.SelectedItem.ToString());
                CurrentUserConn.DisplayCurrentDbTables(Structure.TablesListBox);

                //MessageBox.Show(CurrentUserConn.DbName);
            }
        }

        private bool IsCurrentMainContentAreaSame(UserControl ChosenControl)
        {
            if (!string.IsNullOrWhiteSpace(CurrentUserConn.DbName))
            {
                if (MainInteractionContentArea.Content.GetType() != ChosenControl.GetType())
                {
                    MainInteractionContentArea.Content = ChosenControl;
                    return false;
                }
                else
                    return true;
            }
            else
            {
                MessageBox.Show("No database selected");
                return true;
            }
        }

        private void btn_SQL(object sender, RoutedEventArgs e)
        {
            if (MainInteractionContentArea.Content != SQL)
                MainInteractionContentArea.Content = SQL; // don't need to use IsCurrentMainContentAreaSame (just window for executing sql commands on server)
        }

        private void btn_Structure(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Structure);
        }

        private void btn_Authorized(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Authorized);
        }

        private void btn_Relations(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Relations);
        }

        private void btn_HideShow(object sender, RoutedEventArgs e)
        {

        }
    }
}
