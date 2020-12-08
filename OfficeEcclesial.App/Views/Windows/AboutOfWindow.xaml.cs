using System.Windows;
using OfficeEcclesial.App.ViewModels.Windows;

namespace OfficeEcclesial.App.Views.Windows
{
    public partial class AboutOfWindow
    {
        public AboutOfWindow(AboutOfViewModel viewModel)
        {
            InitializeComponent();

            viewModel.Bind(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as AboutOfViewModel)?.InitializeComponent();
        }
    }
}
