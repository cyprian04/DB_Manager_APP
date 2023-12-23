﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GUI_Database_app.Navigation;

namespace GUI_Database_app.ViewModel
{
    class ServerPanelVM : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private UserControl _currentControl;
        public UserControl CurrentControl
        {
            get => _currentControl;
            set
            {
                _currentControl = value;
                OnPropertyChanged(nameof(CurrentControl));
            }
        }

        public ServerPanelVM(NavigationService navigationService)
        {
            this.navigationService = navigationService;

            navigationService.Register<View.ServerPanelContent.SQLControl>("SQL");
            navigationService.Register<View.ServerPanelContent.StructureControl>("Struct");
            navigationService.Register<View.ServerPanelContent.RelationsControl>("Relations");

            NavigateTo("SQL");
        }

        public ICommand NavigateToCommand => new RelayCommand(param => NavigateTo(param.ToString()));

        private void NavigateTo(string destination)
        {
            UserControl newControl = navigationService.NavigateTo(destination);
            CurrentControl = (CurrentControl == null || CurrentControl.GetType() != newControl.GetType()) ? newControl : CurrentControl;
        }
    }
}