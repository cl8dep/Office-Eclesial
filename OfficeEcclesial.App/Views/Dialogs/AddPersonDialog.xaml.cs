using System.Windows;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.ViewModels.Dialogs;

namespace OfficeEcclesial.App.Views.Dialogs
{
    public partial class AddPersonDialog
    {

        #region Constructor
        public AddPersonDialog(AddPersonDialogViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }
        #endregion

        private void AddPersonDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as AddPersonDialogViewModel)?.InitializeComponent();
        }

        public IPerson ShowDialog(IPerson person = null, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
                Title = title;

            if (person != null)
            {
                (DataContext as AddPersonDialogViewModel)?.Edit(person);
            }

            var result = base.ShowDialog();
            return result == true
                ? (DataContext as AddPersonDialogViewModel)?.GetPerson()
                : null;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
