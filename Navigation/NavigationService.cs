using System;
using System.Collections.Generic;
using System.Windows.Controls;
using GUI_Database_app.ViewModel;

namespace GUI_Database_app.Navigation
{
    public class NavigationService
    {
        private readonly Dictionary<string, Func<UserControl>> userControls;

        public NavigationService()
        {
            userControls = new Dictionary<string, Func<UserControl>>();
        }

        public void Register(string key, Func<UserControl> controlFactory)
        {
            if (!userControls.ContainsKey(key))
            {
                userControls.Add(key, controlFactory);
            }
        }

        public UserControl NavigateTo(string key)
        {
            if (userControls.ContainsKey(key))
            {
                return userControls[key].Invoke();
            }

            return null;
        }
    }
}
