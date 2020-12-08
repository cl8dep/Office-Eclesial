using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Extensions.Wpf;
using OfficeEcclesial.App.Services;
using Prism.Commands;

namespace OfficeEcclesial.App.ViewModels.Windows
{
    public sealed class AboutOfViewModel : BindableBase
    {
        #region Fields
        private string _copyright;
        private string _version;
        private string _busyOpDescription;
        private string _licenseStatus;
        #endregion


        #region Constructor

        public AboutOfViewModel()
        {
        }
        #endregion


        #region Properties
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
        public DelegateCommand SendEmailCommand => new DelegateCommand(OnSendEmailCommand);
        public DelegateCommand SendTelegramCommand => new DelegateCommand(OnSendTelegramCommand);

        private void OnSendEmailCommand()
        {
            Process.Start("mailto:\"Arael D. Espinosa Pérez\"<cl8dep@gmail.com>");
        }
        private void OnSendTelegramCommand()
        {
            Process.Start("https://t.me/cl8dep");
        }

        #endregion

        private void OnCloseCommand()
        {
            CloseAction();
            Application.Shutdown();
        }

        public override void InitializeComponent()
        {
            var info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            Version = $"v{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}";
            Copyright = info.LegalCopyright;

            var bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerAsync();
        }
        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            BusyOpDescription = "Verificando licencia...";
            
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
