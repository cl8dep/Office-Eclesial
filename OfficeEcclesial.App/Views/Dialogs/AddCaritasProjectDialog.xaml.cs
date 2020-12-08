using System;
using System.Windows.Input;
using OfficeEcclesial.App.Entities;
using OfficeEcclesial.App.ViewModels.Dialogs;
using OfficeEcclesial.Database.Entities;

namespace OfficeEcclesial.App.Views.Dialogs
{
    public partial class AddCaritasProjectDialog
    {
        public AddCaritasProjectDialog(AddCaritasProjectDialogViewModel vm)
        {
            InitializeComponent();

            vm.Bind(this);
        }

        public void ShowDialog(CaritasProject item)
        {
            (DataContext as AddCaritasProjectDialogViewModel)?.Edit(item);
            ShowDialog();
        }

    }
}
