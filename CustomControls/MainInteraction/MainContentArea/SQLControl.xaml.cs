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
            // setting string value by getting first and last pointer in the textRange from our SqlCommandTextBox (RichTextBox)
            querryText = new TextRange(SqlCommandTextBox.Document.ContentStart, SqlCommandTextBox.Document.ContentEnd).Text;
            if (CurrentUserConn.ExecuteAndCheckSQLQuerry(querryText, QuerryrResultDataGrid))
                QuerryrResultDataGrid.Visibility = Visibility.Visible;
            else
                QuerryrResultDataGrid.Visibility = Visibility.Hidden;
        }
    }
}
