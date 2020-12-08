using System.Windows;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class LiturgiaMembersSectionView
    {
        public LiturgiaMembersSectionView(LiturgiaMembersSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
           (DataContext as LiturgiaMembersSectionViewModel)?.InitializeComponent();     
        }
    }
}
