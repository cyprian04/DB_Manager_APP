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
using System.Windows.Shapes;

namespace GUI_Database_app
{
    /// <summary>
    /// Logika interakcji dla klasy LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //private void Button_Login_Click(object sender, RoutedEventArgs e)
        //{
        //    if( UserName.Text == "admin" && UserPass.Password == "1234")
        //    {
        //        MainWindow mainWindow = new MainWindow();
        //        mainWindow.Show();
        //        this.Close();
        //    }
        //    else
        //    {
        //        UserName.Text = "";
        //        UserPass.Password = "";
        //        tbLoginResult.Text = "Invalid data, try again";
        //    }
        //}
    }
}
