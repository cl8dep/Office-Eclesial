using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeEcclesial.App.Database;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Utils;
using OfficeEcclesial.App.Views;
using OfficeEcclesial.App.Views.Dialogs;
using OfficeEcclesial.App.Views.SplashScreen;

namespace OfficeEcclesial.App
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Services = ConfigureServices();
            Provider = Services.BuildServiceProvider();

            MainWindow = Provider.GetService<MainWindow>();

            Provider.GetService<SplashScreenWindow>().ShowDialog();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es");
        }

        private IConfiguration LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            return configuration;
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var configuration = LoadConfiguration();

            services.AddDbContext<MainDatabase>(options => options.UseSqlite(AppEnvironment.MainDatabaseConnectionString));

            services.AddSingleton(configuration);

            var views = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Namespace != null && x.IsClass && x.Namespace.Contains("Views"));

            foreach (var item in views)
            {
                services.AddTransient(item);
            }

            var viewModels = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Namespace != null && x.IsClass && x.Namespace.Contains("ViewModels"));

            foreach (var item in viewModels)
            {
                services.AddTransient(item);
            }

            var ourServices = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Namespace != null && x.IsClass && x.Namespace.Contains("Services"));

            foreach (var item in ourServices)
            {
                services.AddTransient(item);
            }

            services.AddTransient<CaritasMembersService>();

            return services;
        }


        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var builder = new StringBuilder();
            builder.Append("Mensaje: ");
            builder.Append(e.Exception.Message);
            builder.Append(Environment.NewLine);

            builder.Append("Source: ");
            builder.Append(e.Exception.Source);
            builder.Append(Environment.NewLine);

            builder.Append("StackTrace: ");
            builder.Append(e.Exception.StackTrace);
            builder.Append(Environment.NewLine);

            builder.Append("TargetSite: ");
            builder.Append(e.Exception.TargetSite);
            builder.Append(Environment.NewLine);


            MessageBox.Show(builder.ToString(), "Error fatal", MessageBoxButton.OK, MessageBoxImage.Error,
                MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        }

        
    }
}
