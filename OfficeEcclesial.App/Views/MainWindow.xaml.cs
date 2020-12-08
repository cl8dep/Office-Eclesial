using System;
using System.Windows.Media;
using OfficeEcclesial.App.ViewModels;

namespace OfficeEcclesial.App.Views
{
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            viewModel.Bind(this);
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            Extensions.Wpf.Application.Shutdown();
        }

    }
}
