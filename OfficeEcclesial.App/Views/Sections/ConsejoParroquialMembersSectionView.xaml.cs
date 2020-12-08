using System.Windows;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class ConsejoParroquialMembersSectionView
    {
        public ConsejoParroquialMembersSectionView(ConsejoParroquialMembersSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
           (DataContext as ConsejoParroquialMembersSectionViewModel)?.InitializeComponent();     
        }
    }
}
