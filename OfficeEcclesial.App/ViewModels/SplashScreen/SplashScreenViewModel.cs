using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Utils;
using OfficeEcclesial.App.Views;

using Prism.Commands;

using Application = OfficeEcclesial.App.Extensions.Wpf.Application;

namespace OfficeEcclesial.App.ViewModels.SplashScreen
{
    public sealed class SplashScreenViewModel : BindableBase
    {
        #region Fields

        private string _copyright;
        private string _version;
        private string _busyOpDescription;
        private string _licenseStatus;

        #endregion

        #region Constructor

        public SplashScreenViewModel()
        {
            
        }

        #endregion

        #region Properties

        public bool LoadingComplete { get; set; }

        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                RaisePropertyChanged(nameof(Version));
            }
        }

        public string Copyright
        {
            get => _copyright;
            set
            {
                _copyright = value;
                RaisePropertyChanged(nameof(Copyright));
            }
        }

        public string BusyOpDescription
        {
            get => _busyOpDescription;
            set
            {
                _busyOpDescription = value;
                RaisePropertyChanged(nameof(BusyOpDescription));
            }
        }

        public string LicenseStatus
        {
            get => _licenseStatus;
            set
            {
                _licenseStatus = value;
                RaisePropertyChanged(nameof(LicenseStatus));
            }
        }

        #endregion

        #region Commands

        public DelegateCommand CloseCommand => new DelegateCommand(OnCloseCommand);

        #endregion

        private void OnCloseCommand()
        {
            CloseAction();
            Application.Shutdown();
        }

        public override void InitializeComponent()
        {
            BusyOpDescription = "Cargando...";
            var info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            Version =
                $"v{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}";
            Copyright = info.LegalCopyright;

            var bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingComplete = true;
            BusyOpDescription = string.Empty;
            CloseAction();
            Application.GetService<MainWindow>().Show();
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {            
            if (!AppEnvironment.DatabaseExist())
            {
                BusyOpDescription = "Creando base de datos...";               
                AppEnvironment.CreateEmptyDatabase();
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));
            BusyOpDescription = "Inicializando...";
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}