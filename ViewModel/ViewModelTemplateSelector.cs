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
        public DataTemplate LoginTemplate { get; set; }
        public DataTemplate HomeTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is LoginFormVM)
            {
                return LoginTemplate;
            }
            // others here
            // Dodaj inne przypadki dla innych ViewModeli/UserControli

            return base.SelectTemplate(item, container);
        }
    }


}
