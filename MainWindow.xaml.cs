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
using System.Data;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;


namespace GUI_Database_app
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data.Connection conn = new Data.Connection();
        MySqlCommand query = new MySqlCommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnToggle_Run_Click(object sender, RoutedEventArgs e)
        {
            await StartLoadingAnimation(); // try catch finally wykona się po zakończeniu animacji w funkcji 

            try
            {
                if(DB_name.Text !="")
                {
                    conn.Initialize("127.0.0.1", DB_name.Text, "root", "");
                    Data.Connection.dataSource();
                    conn.connOpen();

                    tbConResult.Text = "Sucessfully connected";
                    tbConResult.Foreground = Brushes.Green;

                    tbErrorMsg.Text = "";
                    tbErrorBrd.Visibility = Visibility.Hidden;
                }
                else
                {
                    tbConResult.Foreground = Brushes.Red;
                    tbConResult.Text = "no database selected";
                }
            }
            catch (MySqlException ex)
            {
                tbConResult.Text = "Failed to connect";
                tbConResult.Foreground = Brushes.Red;

                tbErrorMsg.Foreground = Brushes.Red;
                tbErrorMsg.Text = "Error" + ex.Message;
                tbErrorBrd.Visibility = Visibility.Visible;
            }
            finally
            {
                conn.connClose();
            }
        }

        private async Task StartLoadingAnimation() // stworzenie pure async function 
        {
            loadProgressBar.Visibility = Visibility.Visible; // Pokaż pasek postępu
            loadProgressBar.Value = 0; // Zresetuj wartość paska postępu

            DoubleAnimation animation = new DoubleAnimation{From = 0, To = 100, Duration = TimeSpan.FromSeconds(1)};

            loadProgressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
