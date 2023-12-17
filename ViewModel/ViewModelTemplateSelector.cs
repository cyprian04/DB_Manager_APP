using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GUI_Database_app.ViewModel
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HomeTemplate { get; set; }
        public DataTemplate DbSettingsTemplate { get; set; }
       // public DataTemplate MainInteractionTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is HomeVM)
            {
                return HomeTemplate;
            }
            else if (item is DbSettingsVM)
            {
                return DbSettingsTemplate;
            }
            //else if (item is MainInteractionVM)
            //{
            //    return MainInteractionTemplate;
            //}
            // others here
            // Dodaj inne przypadki dla innych ViewModeli/UserControli

            return base.SelectTemplate(item, container);
        }
    }
}
