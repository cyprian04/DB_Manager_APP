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
        MainInteraction.MainContentArea.SQLControl SQL = null;
        MainInteraction.MainContentArea.AuthorizedControl Authorized = null;
        MainInteraction.MainContentArea.RelationsControl Relations = null;

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
                //
                Structure = new MainInteraction.MainContentArea.StructureControl();
                SQL = new MainInteraction.MainContentArea.SQLControl();
                Authorized = new MainInteraction.MainContentArea.AuthorizedControl();
                Relations = new MainInteraction.MainContentArea.RelationsControl();
            }
        }

        private void IsCurrentMainContentAreaSame(UserControl ChosenControl)
        {
            if (!string.IsNullOrWhiteSpace(CurrentUserConn.DbName))
            {
                if (MainInteractionContentArea.Content.GetType() != ChosenControl.GetType())
                    MainInteractionContentArea.Content = ChosenControl;
            }
            else
                MessageBox.Show("No database selected");
        }

        private void btn_Structure(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Structure);
        }

        private void btn_SQL(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(SQL);
        }

        private void btn_Authorized(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Authorized);
        }

        private void btn_Relations(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Relations);
        }
    }
}
