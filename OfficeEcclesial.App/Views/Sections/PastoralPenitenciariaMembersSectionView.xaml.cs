using System.Windows;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class PastoralPenitenciariaMembersSectionView
    {
        public PastoralPenitenciariaMembersSectionView(PastoralPenitenciariaMembersSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
           (DataContext as PastoralPenitenciariaMembersSectionViewModel)?.InitializeComponent();     
        }
    }
}
