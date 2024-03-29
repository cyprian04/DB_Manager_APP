﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_Database_app.ViewModel.ServerPanelVMs
{
    class StructureVM : ViewModelBase
    {
        private static ImageBrush show = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-show.png", UriKind.RelativeOrAbsolute)));
        private static ImageBrush hide = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/icon-hide.png", UriKind.RelativeOrAbsolute)));
        private static CornerRadius adjutedRightCol = new CornerRadius(0, 0, 9, 9);
        private static CornerRadius defaultRightCol = new CornerRadius(0, 0, 9, 0);

        private static Thickness defaultBorder = new Thickness(2, 2, 0, 0);
        private static Thickness adjustedBorder = new Thickness(0, 2, 0, 0);
        private static GridLength defaultLeftColWidth = new GridLength(200);
        private static GridLength adjustedLeftColWidth = new GridLength(0);
        
        private ImageBrush hideShowBtn = show;
        private CornerRadius currentRadius = defaultRightCol;        
        private CornerRadius mainAreaRadius = defaultRightCol;
        private Thickness currentThickness = defaultBorder;
        private Thickness mainCurrentThickness = defaultBorder;
        private GridLength currentContentColWidth = defaultLeftColWidth;

        private readonly Data.DBServerContent dbServerContent;
        private string currentContent = null;
        private ObservableCollection<string> tableListBox = new ObservableCollection<string>();
        private DataTable querryResult;
        private bool isVisible = false;
        private string selectedItem;
        private string currentTB = "Not choosen";

        public ImageBrush HideShowBtn
        {
            get => hideShowBtn;
            set
            {
                hideShowBtn = value;
                OnPropertyChanged(nameof(HideShowBtn));
            }
        }
        public CornerRadius CurrentRadius
        {
            get => currentRadius;
            set
            {
                currentRadius = value;
                OnPropertyChanged(nameof(CurrentRadius));
            }
        }
        public CornerRadius MainAreaRadius
        {
            get => mainAreaRadius;
            set
            {
                mainAreaRadius = value;
                OnPropertyChanged(nameof(MainAreaRadius));
            }
        }
        public Thickness CurrentThickness
        {
            get => currentThickness;
            set
            {
                currentThickness = value;
                OnPropertyChanged(nameof(CurrentThickness));
            }
        }
        public Thickness MainCurrentThickness
        {
            get => mainCurrentThickness;
            set
            {
                mainCurrentThickness = value;
                OnPropertyChanged(nameof(MainCurrentThickness));
            }
        }
        public GridLength CurrentContentColWidth
        {
            get => currentContentColWidth;
            set
            {
                currentContentColWidth = value;
                OnPropertyChanged(nameof(CurrentContentColWidth));
            }
        }
        
        public ObservableCollection<string> TableListBox
        {
            get { return tableListBox; }
            set
            {
                if (tableListBox != value)
                {
                    tableListBox = value;
                    OnPropertyChanged(nameof(TableListBox));
                }
            }
        }
        public DataTable QuerryResult
        {
            get { return querryResult; }
            set
            {
                querryResult = value;
                OnPropertyChanged(nameof(QuerryResult));
            }
        }
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value == null)
                {
                    selectedItem = value;
                    CurrentTB = "Not choosen";
                }
                else if (selectedItem != value && value != null)
                {
                    selectedItem = value;
                    CurrentTB = selectedItem;
                }                    
                if(QuerryResult != null) QuerryResult.Clear();
                OnPropertyChanged(nameof(SelectedItem));
                currentContent = null;
                IsVisible = false;
            }
        }
        public string CurrentTB
        {
            get { return currentTB; }
            set
            {
                if (currentTB != value)
                {
                    currentTB = value;
                    OnPropertyChanged(nameof(CurrentTB));
                }
            }
        }

        public StructureVM(Data.DBServerContent dbServerContent, ServerPanelVM serverPanelVM)
        {
            this.dbServerContent = dbServerContent;
            dbServerContent.collectionTables = TableListBox;

            serverPanelVM.SelectedItemChanged += ServerPanelVM_SelectedItemChanged;
            serverPanelVM.HideShowBtnChanged += ServerPanelVM_HideShowBtnChanged;
        }

        private void ServerPanelVM_SelectedItemChanged(object sender, string selectedItem)
        {
            if (selectedItem != null) dbServerContent.DisplayCurrentListBox("TABLES", tableListBox);
            else tableListBox.Clear();
        }

        private void ServerPanelVM_HideShowBtnChanged(object sender, string selectedItem)
        {
            MainAreaRadius = MainAreaRadius != adjutedRightCol ? adjutedRightCol : defaultRightCol;
            MainCurrentThickness = MainCurrentThickness != adjustedBorder ? adjustedBorder : defaultBorder;
        }

        public ICommand ShowStructCommand => new RelayCommand(param => ShowTable(param.ToString()));
        public ICommand ShowDataCommand => new RelayCommand(param => ShowTable(param.ToString()));
        public ICommand ShowHideCommand => new RelayCommand(ShowHide);

        private void ShowTable(string content)
        {
            if (currentContent != content && CurrentTB != "Not choosen")
            {
                QuerryResult = dbServerContent.ExecuteAndCheckSQLQuerry(content + currentTB + ";");
                IsVisible = QuerryResult.Rows.Count == 0 ? false : true;
                currentContent = content;
            }
        }

        private void ShowHide(object parameter)
        {
            HideShowBtn = HideShowBtn != hide ? hide : show;
            CurrentRadius = CurrentRadius != adjutedRightCol ? adjutedRightCol : defaultRightCol;
            CurrentThickness = currentThickness != adjustedBorder ? adjustedBorder : defaultBorder;
            CurrentContentColWidth = CurrentContentColWidth != adjustedLeftColWidth ? adjustedLeftColWidth : defaultLeftColWidth;
        }
    }
}
