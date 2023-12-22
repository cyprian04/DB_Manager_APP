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

namespace GUI_Database_app.View.MainInteractionContent
{
    /// <summary>
    /// Logika interakcji dla klasy RelationsControl.xaml
    /// </summary>
    public partial class RelationsControl : UserControl
    {
        Data.Connection CurrentUserConn = null;

        public RelationsControl()
        {
            InitializeComponent();
        }

        public RelationsControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
            CurrentUserConn = CurrentUserConn_in;
        }
    }
}
