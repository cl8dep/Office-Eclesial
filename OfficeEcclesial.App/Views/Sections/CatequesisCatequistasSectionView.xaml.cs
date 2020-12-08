using System.Windows;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class CatequesisCatequistasSectionView
    {
        public CatequesisCatequistasSectionView(CatequesisCatequistasSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
           (DataContext as CatequesisCatequistasSectionViewModel)?.InitializeComponent();     
        }
    }
}
