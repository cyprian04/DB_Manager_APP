using System;
using System.Collections.Generic;
using GUI_Database_app.ViewModel;

namespace GUI_Database_app.Navigation
{
    public class NavigationService
    {
        private readonly Dictionary<string, Func<ViewModelBase>> viewModels;
        private ViewModelBase currentViewModel;

        public NavigationService()
        {
            viewModels = new Dictionary<string, Func<ViewModelBase>>();
        }

        public void RegisterViewModel(string key, Func<ViewModelBase> viewModelFactory)
        {
            if (!viewModels.ContainsKey(key))
            {
                viewModels.Add(key, viewModelFactory);
            }
        }

        public void NavigateTo(string key)
        {
            if (viewModels.ContainsKey(key))
            {
                currentViewModel = viewModels[key].Invoke();
                OnNavigationChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ViewModelBase CurrentViewModel => currentViewModel;

        public event EventHandler OnNavigationChanged;
    }

}
