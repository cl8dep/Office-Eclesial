

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OfficeEcclesial.App.Extensions.Mvvm
{
    public class BindableBase : Prism.Mvvm.BindableBase
    {
        public virtual void InitializeComponent()
        {

        }
        public Action CloseAction { get; set; }
        public Dispatcher Dispatcher { get; set; }

        public void Bind(Window window)
        {
            CloseAction = window.Close;
            Dispatcher = window.Dispatcher;
            window.DataContext = this;
        }
        public void Bind(UserControl control)
        {
            Dispatcher = control.Dispatcher;
            control.DataContext = this;
        }
    }
}
