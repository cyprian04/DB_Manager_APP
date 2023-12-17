using System;
using System.Collections.Generic;
using System.Windows.Controls;
using SimpleInjector;

namespace GUI_Database_app.Navigation
{
    public class NavigationService
    {
        private readonly Container container;
        private readonly Dictionary<string, Func<UserControl>> userControls;

        public NavigationService(Container container)
        {
            userControls = new Dictionary<string, Func<UserControl>>();
            this.container = container;
        }

        public void Register<T>(string key) where T : UserControl
        {
            if (!userControls.ContainsKey(key))
            {
                userControls.Add(key, () => container.GetInstance<T>());
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
