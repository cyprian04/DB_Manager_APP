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

namespace GUI_Database_app.View.ServerPanelContent
{
    /// <summary>
    /// Logika interakcji dla klasy StructureControl.xaml
    /// </summary>
    public partial class StructureControl : UserControl
    {
        Data.Connection CurrentUserConn = null;
        ImageBrush show = null;
        ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));

        CornerRadius adjutedRightCol = new CornerRadius(0,0,9,0);
        CornerRadius defaultRightCol = new CornerRadius(9, 0, 9, 0);

        GridLength defaultLeftColWidth = new GridLength(200);
        GridLength adjustedLeftColWidth = new GridLength(0);

        Thickness defaultBorder = new Thickness(2, 2, 0, 0);
        Thickness adjustedBorder = new Thickness(0, 2, 0, 0);
        
        private bool hidden = false;
        private string querry = null;

        public StructureControl()
        {
            InitializeComponent();
            show = HideShowBtn.Background as ImageBrush; // getting the path of the imageSource set as default at start up
        }

        private void btn_HideShow(object sender, RoutedEventArgs e)
        {
            if (!hidden)
            {
                HideShowBtn.Background = hide;
                LeftContentColumn.Width = adjustedLeftColWidth;
                RightContentColumn.SetValue(Grid.ColumnProperty, 0);

                RightContentColumnBorder.CornerRadius = adjutedRightCol;
                RightContentColumnBorder.BorderThickness = adjustedBorder;
                hidden = true;
            }
            else
            {
                HideShowBtn.Background = show;
                LeftContentColumn.Width = defaultLeftColWidth;

                RightContentColumnBorder.CornerRadius = defaultRightCol;
                RightContentColumnBorder.BorderThickness = defaultBorder;
                hidden = false;
            }
        }

        private void TablesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TablesListBox.SelectedItem != null)
            {
                CurrentUserConn.TbName = TablesListBox.SelectedItem.ToString();
                TableStructureDataGrid.Visibility = Visibility.Hidden;
                TableStructureDataGrid.ItemsSource = null;
                CurrentTableTextBlock.Text = TablesListBox.SelectedItem.ToString();
            }
        }

        private void Struct_btn(object sender, RoutedEventArgs e)
        {
            if (querry != "DESCRIBE " + CurrentUserConn.TbName + ";")
            {
                querry = "DESCRIBE " + CurrentUserConn.TbName +";";
                //CurrentUserConn.ExecuteAndCheckSQLQuerry(Data.Connection.TypeOfQuerry.ShowStruct, querry, TableStructureDataGrid);
            }
        }

        private void Data_btn(object sender, RoutedEventArgs e)
        {
            if (querry != "SELECT * FROM " + CurrentUserConn.TbName + ";")
            {
                querry = "SELECT * FROM " + CurrentUserConn.TbName + ";";
                //CurrentUserConn.ExecuteAndCheckSQLQuerry(Data.Connection.TypeOfQuerry.ShowData, querry, TableStructureDataGrid);
            }
        }
    }
}
