using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace OfficeEcclesial.App.Extensions.Mvvm
{
    public static class ViewModelExtensions
    {
        public static UserControl BindToolbar(this UserControl control, UserControl toolbar)
        {
            toolbar.DataContext = control.DataContext;
            return toolbar;
        }

    }
}
