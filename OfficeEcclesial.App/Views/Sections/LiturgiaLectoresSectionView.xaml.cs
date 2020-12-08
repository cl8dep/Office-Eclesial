using System.Windows;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class LiturgiaLectoresSectionView
    {
        public LiturgiaLectoresSectionView(LiturgiaLectoresSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        private void CaritasMembersSectionView_OnLoaded(object sender, RoutedEventArgs e)
        {
           (DataContext as LiturgiaLectoresSectionViewModel)?.InitializeComponent();     
        }
    }
}
