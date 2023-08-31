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

namespace GUI_Database_app.CustomControls.MainInteraction.MainContentArea
{
    /// <summary>
    /// Logika interakcji dla klasy SQLControl.xaml
    /// </summary>
    public partial class SQLControl : UserControl
    {
        Data.Connection CurrentUserConn = null;
        string querryText = null;
        public SQLControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
            CurrentUserConn = CurrentUserConn_in;
        }

        private void btn_ExecuteSqlCommand(object sender, RoutedEventArgs e)
        {
           // querryText = 
           // CurrentUserConn.ExecuteSqlQuerry();
        }
    }
}
