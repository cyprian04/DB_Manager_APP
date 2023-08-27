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
    /// Logika interakcji dla klasy ViewControl.xaml
    /// </summary>
    public partial class ViewControl : UserControl
    {
        public ViewControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
        }
    }
}
