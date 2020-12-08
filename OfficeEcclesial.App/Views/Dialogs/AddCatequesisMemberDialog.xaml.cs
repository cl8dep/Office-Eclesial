using System.Windows;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.ViewModels.Dialogs;

namespace OfficeEcclesial.App.Views.Dialogs
{
    public partial class AddCatequesisMemberDialog
    {

        #region Constructor
        public AddCatequesisMemberDialog(AddCatequesisMemberDialogViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }
        #endregion

        private void AddPersonDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as AddCatequesisMemberDialogViewModel)?.InitializeComponent();
        }

        public CatequesisMember ShowDialog(CatequesisMember person = null, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
                Title = title;

            if (person != null)
            {
                (DataContext as AddCatequesisMemberDialogViewModel)?.Edit(person);
            }

            var result = base.ShowDialog();
            return result == true
                ? (DataContext as AddCatequesisMemberDialogViewModel)?.GetEntity()
                : null;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
