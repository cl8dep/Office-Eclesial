using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Extensions.Wpf;
using Prism.Commands;

namespace OfficeEcclesial.App.ViewModels.Sections
{
    public sealed class SettingsSectionViewModel : BindableBase
    {
        #region Fields
        private bool _isLoading;
        private string _principalDatabaseSize;
        private string _principalDatabaseLastModificationDate;
        private bool _isExporting;
        #endregion

        #region Bindable properties
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
            }
        }
        public string PrincipalDatabaseSize
        {
            get => _principalDatabaseSize;
            set
            {
                _principalDatabaseSize = value; 
                RaisePropertyChanged(nameof(PrincipalDatabaseSize));
            }
        }
        public string PrincipalDatabaseLastModificationDate
        {
            get => _principalDatabaseLastModificationDate;
            set
            {
                _principalDatabaseLastModificationDate = value;
                RaisePropertyChanged(nameof(PrincipalDatabaseLastModificationDate));
            }
        }
        public bool IsExporting
        {
            get => _isExporting;
            set
            {
                _isExporting = value;
                RaisePropertyChanged(nameof(IsExporting));
            }
        }
        #endregion

        #region Bindable Commands
        public DelegateCommand ExportDatabaseCommand => new DelegateCommand(OnExportDatabaseCommand);
        #endregion

        #region On Bindable Commands functions
        private void OnExportDatabaseCommand()
        {
            var exportingBackgroundWorker = new BackgroundWorker();
            exportingBackgroundWorker.DoWork += ExportingBackgroundWorkerOnDoWork;
            exportingBackgroundWorker.RunWorkerCompleted += ExportingBackgroundWorkerOnRunWorkerCompleted;
            exportingBackgroundWorker.RunWorkerAsync();
        }
        #endregion

        #region Override functions
        public override void InitializeComponent()
        {
            IsLoading = true;
            var bg = new BackgroundWorker();
            bg.DoWork += BgOnDoWork;
            bg.RunWorkerAsync();
        }
        #endregion

        #region Private functions
        private void ExportingBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsExporting = false;
        }
        private void ExportingBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            var connectionString = Application.GetService<IConfiguration>()["Database:ConnectionString"];
            var path = Path.Combine(Environment.CurrentDirectory, connectionString);

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "SQLite Database (*.db)|*.db",
                ValidateNames = true,
                Title = "Guardar base de datos como"
            };

            var result = saveFileDialog.ShowDialog();
            if (result == null || !result.Value) return;

            Dispatcher?.Invoke(() =>
            {
                IsExporting = true;
            });

            var readStream = File.OpenRead(path);
            var writeStream = File.Create(saveFileDialog.FileName);

            var buffer = new byte[120 * 120];

            while (readStream.Position != readStream.Length)
            {
                readStream.Read(buffer, 0, buffer.Length);
                writeStream.Write(buffer, 0, buffer.Length);
            }

            Process.Start("C:\\Windows\\explorer.exe", $"/select,\"{saveFileDialog.FileName}\"");

            readStream.Close();
            writeStream.Close();
        }
        private void BgOnDoWork(object sender, DoWorkEventArgs e)
        {
            var connectionString = Application.GetService<IConfiguration>()["Database:ConnectionString"];
            var path = Path.Combine(Environment.CurrentDirectory, connectionString);
            if (!File.Exists(path)) return;
            var info = new FileInfo(path);
            Dispatcher.InvokeAsync(() =>
            {
                PrincipalDatabaseSize = FormatSize(info.Length);
                PrincipalDatabaseLastModificationDate = info.LastWriteTime.ToShortDateString();
            });
        }
        private string FormatSize(long infoLength)
        {
            var count = 0;
            while (infoLength >= 1024)
            {
                infoLength /= 1024;
                count++;
            }

            var arg = string.Empty;
            switch (count)
            {
                case 0:
                    arg = "B";
                    break;
                case 1:
                    arg = "Kb";
                    break;
                case 2:
                    arg = "Mb";
                    break;
                case 3:
                    arg = "Gb";
                    break;
            }

            return $"{infoLength} {arg}";
        }
        #endregion
    }
}
