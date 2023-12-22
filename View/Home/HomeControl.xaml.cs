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

namespace GUI_Database_app.View.Home
{
    /// <summary>
    /// Logika interakcji dla klasy HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            HomeMediaVideo.Play();
        }

        private void HomeMediaVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (HomeMediaVideo.Source != null)
            {
                HomeMediaVideo.Position = TimeSpan.Zero; // Reset the position to the beginning
                HomeMediaVideo.Play(); // Start playing again
            }
        }
    }
}

