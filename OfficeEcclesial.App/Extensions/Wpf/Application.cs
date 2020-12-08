using System;
using Microsoft.Extensions.DependencyInjection;

namespace OfficeEcclesial.App.Extensions.Wpf
{
    public class Application : System.Windows.Application
    {
        public IServiceCollection Services { get; set; }
        public IServiceProvider Provider { get; set; }

        public new static Application Current => System.Windows.Application.Current as Application;

        public static TWindow GetService<TWindow>()
        {
            return Current.Provider.GetService<TWindow>();
        }

        public new static void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
