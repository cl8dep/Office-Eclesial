using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeEcclesial.App.ViewModels.Sections;

namespace OfficeEcclesial.App.Views.Sections
{
    public partial class CaritasProjectsSectionView : UserControl
    {
        public CaritasProjectsSectionView(CaritasProjectsSectionViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        private void CaritasProjectsSectionView_OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as CaritasProjectsSectionViewModel)?.InitializeComponent();
        }
    }
}
