using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace GUI_Database_app.View
{
    public partial class ServerPanelControl : UserControl
    {
        public ServerPanelControl()
        {
            InitializeComponent();
        }

        private void DatabasesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // if (DatabasesListBox.SelectedItem != null)
           // {
           //     if (MainInteractionContentArea.Content == Structure) Structure.TableStructureDataGrid.Visibility = Visibility.Hidden;
           // 
           //     CurrentUserConn.ConnectionWithDb(DatabasesListBox.SelectedItem.ToString());
           //     CurrentUserConn.DisplayCurrentListBox(Structure.TablesListBox);
           //     CurrentDBTextBlock.Text = DatabasesListBox.SelectedItem.ToString();
           // }
        }
        
        private void btn_Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File",
                Filter = "All Files (*.*)|*.*"
            };
        
            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show("Selected file: " + openFileDialog.FileName);
                //CurrentUserConn.ImportDB(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Abortet action");
            }
        }
        
        private void btn_Export(object sender, RoutedEventArgs e)
        {
           // if (DatabasesListBox.SelectedItem != null) CurrentUserConn.ExportDB(DatabasesListBox.SelectedItem.ToString());
           // else MessageBox.Show("No database selected");
        }
    }
}
