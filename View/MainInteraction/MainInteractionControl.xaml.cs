﻿using System;
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
using Microsoft.Win32;

namespace GUI_Database_app.View.MainInteraction
{
    /// <summary>
    /// Logika interakcji dla klasy SqlCommandControl.xaml
    /// </summary>
    public partial class MainInteractionControl : UserControl
    {
       Data.Connection CurrentUserConn = null;
       MainContentArea.StructureControl  Structure = null;
       MainContentArea.RelationsControl Relations = null;
       MainContentArea.SQLControl SQL = null;

        ImageBrush show = null;
        ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));

        GridLength defaultLeftColWidth = new GridLength(200);
        GridLength adjustedLeftColWidth = new GridLength(0);

        bool hidden = false;

        public MainInteractionControl()
        {
            InitializeComponent();
            show = HideShowBtn.Background as ImageBrush;
            MainInteractionContentArea.Content = SQL;
        }

        // public MainInteractionControl(Data.Connection CurrentUserConn_in)
        // {
        //     InitializeComponent();
        //     CurrentUserConn = CurrentUserConn_in;
        //     CurrentUserConn.DisplayCurrentListBox(DatabasesListBox);
        //     show = HideShowBtn.Background as ImageBrush; 
        //
        //
        //     SQL = new MainContentArea.SQLControl(CurrentUserConn);
        //     Structure = new MainContentArea.StructureControl(CurrentUserConn);
        //     Relations = new MainContentArea.RelationsControl(CurrentUserConn);
        //
        //     MainInteractionContentArea.Content = SQL;
        // }

        private void DatabasesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatabasesListBox.SelectedItem != null)
            {
                if (MainInteractionContentArea.Content == Structure) Structure.TableStructureDataGrid.Visibility = Visibility.Hidden;
            
                CurrentUserConn.ConnectionWithDb(DatabasesListBox.SelectedItem.ToString());
                CurrentUserConn.DisplayCurrentListBox(Structure.TablesListBox);
                CurrentDBTextBlock.Text = DatabasesListBox.SelectedItem.ToString();
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
                {
                    MessageBox.Show("No database selected");
                    return true;
                }
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
        
        private void btn_Relations(object sender, RoutedEventArgs e)
        {
            IsCurrentMainContentAreaSame(Relations);
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
                CurrentUserConn.ImportDB(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Abortet action");
            }
        }
        
        private void btn_Export(object sender, RoutedEventArgs e)
        {
            if (DatabasesListBox.SelectedItem != null) CurrentUserConn.ExportDB(DatabasesListBox.SelectedItem.ToString());
            else MessageBox.Show("No database selected");
        }
        
        private void btn_HideShow(object sender, RoutedEventArgs e)
        {
            if (!hidden)
            {
                HideShowBtn.Background = hide;
                LeftContentColumn.Width = adjustedLeftColWidth;
                RightContentColumn.SetValue(Grid.ColumnProperty, 0);
        
                hidden = true;
            }
            else
            {
                HideShowBtn.Background = show;
                LeftContentColumn.Width = defaultLeftColWidth;
        
                hidden = false;
            }
        }
    }
}