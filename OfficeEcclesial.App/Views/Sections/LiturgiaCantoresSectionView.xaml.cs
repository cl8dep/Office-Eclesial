using System.Windows;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class LiturgiaCantoresSectionView
    {
        public LiturgiaCantoresSectionView(LiturgiaCantoresSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);

        }

        private void CaritasMembersSectionView_OnLoaded(object sender, RoutedEventArgs e)
        {
           (DataContext as LiturgiaCantoresSectionViewModel)?.InitializeComponent();     
        }
    }
}
