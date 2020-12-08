using System.Windows;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.ViewModels.Dialogs;

namespace OfficeEcclesial.App.Views.Dialogs
{
    public partial class AddCaritasMemberDialog
    {

        #region Constructor
        public AddCaritasMemberDialog(AddCaritasMemberDialogViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }
        #endregion

        private void AddPersonDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as AddCaritasMemberDialogViewModel)?.InitializeComponent();
        }

        public CaritasMember ShowDialog(CaritasMember member = null)
        {
            if (member != null)
            {
                (DataContext as AddCaritasMemberDialogViewModel)?.Edit(member);
            }

            var result = base.ShowDialog();
            return result == true
                ? (DataContext as AddCaritasMemberDialogViewModel)?.GetEntity()
                : null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
