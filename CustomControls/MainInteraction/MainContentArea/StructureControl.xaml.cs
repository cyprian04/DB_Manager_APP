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
    /// Logika interakcji dla klasy StructureControl.xaml
    /// </summary>
    public partial class StructureControl : UserControl
    {
        Data.Connection CurrentUserConn = null;
        ImageBrush show = null;
        ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));

        CornerRadius rightColFullVisible = new CornerRadius(0,0,9,0);
        CornerRadius rightColNotFullVisible = new CornerRadius(9, 0, 9, 0);

        GridLength defaultLeftColWidth = new GridLength(200);
        GridLength hiddenLeftColWidth = new GridLength(0);

        private bool Hidden = false;

        public StructureControl(Data.Connection CurrentUserConn_in)
        {
            InitializeComponent();
            CurrentUserConn = CurrentUserConn_in;
            show = HideShowBtn.Background as ImageBrush; // getting the path of the imageSource set as default at start up
        }

        private void btn_HideShow(object sender, RoutedEventArgs e)
        {
            if (!Hidden)
            {
                HideShowBtn.Background = hide;

                TablesLeftColumn.Width = hiddenLeftColWidth;
                ContentRightColumn.SetValue(Grid.ColumnProperty, 0);

                RightContentBorder.CornerRadius = rightColFullVisible;
                Hidden = true;
            }
            else
            {
                HideShowBtn.Background = show;
                TablesLeftColumn.Width = defaultLeftColWidth;

                RightContentBorder.CornerRadius = rightColNotFullVisible;

                Hidden = false;
            }
        }
    }
}
