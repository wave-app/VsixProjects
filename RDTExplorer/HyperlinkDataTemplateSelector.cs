using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RDTExplorer
{
    class HyperlinkDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RegularTemplate { get; set; }

        public DataTemplate HyperlinkTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var str = item as string;

            // Check if str contains path and return the dataTemplate accordingly

            return HyperlinkTemplate;// Either RegularTemplate or HyperlinkTemplate 
       }
    }
}
