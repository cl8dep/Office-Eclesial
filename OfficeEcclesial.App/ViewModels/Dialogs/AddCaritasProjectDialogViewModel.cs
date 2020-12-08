using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Views.Dialogs;
using OfficeEcclesial.App.Views.Messages;
using OfficeEcclesial.Database.Entities;
using Prism.Commands;

namespace OfficeEcclesial.App.ViewModels.Dialogs
{
    public sealed class AddCaritasProjectDialogViewModel : BindableBase, IDataErrorInfo
    {
        #region Fields
        private string _projectName;
        private ICollectionView _beneficiaries;
        private ObservableCollection<IPerson> _beneficiariesCollection;
        private bool _isLoading;
        private IPerson _person;
        #endregion

        #region Bindable properties
        [RegularExpression("^[a-zA-Z ]{1,25}$", ErrorMessage = "El texto no cumple el formato especificado.")]
        [Required(ErrorMessage = "Usted debe proveer un valor para este campo.")]
        public string ProjectName
        {
            get => _projectName;
            set
            {
                _projectName = value;
                RaisePropertyChanged(nameof(ProjectName));
            }
        }
        public IPerson Representant
        {
            get => _person;
            set
            {
                _person = value;
                RaisePropertyChanged(nameof(Representant));
                RaisePropertyChanged(nameof(RepresentantName));
            }
        }
        public string RepresentantName => Representant?.ToString();
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
            }
        }
        public ObservableCollection<IPerson> BeneficiariesCollection
        {
            get => _beneficiariesCollection;
            set
            {
                _beneficiariesCollection = value;
                RaisePropertyChanged(nameof(BeneficiariesCollection));
            }
        }
        public ICollectionView Beneficiaries
        {
            get => _beneficiaries;
            set
            {
                _beneficiaries = value;
                RaisePropertyChanged(nameof(Beneficiaries));
            }
        }
        #endregion

        #region Properties
        public bool IsEdition { get; set; }
        public int EditionId { get; set; }
        public CaritasProjectsService Service { get; }
        #endregion

        #region Constructor

        public AddCaritasProjectDialogViewModel(CaritasProjectsService service)
        {
            BeneficiariesCollection = new ObservableCollection<IPerson>();
            Beneficiaries = CollectionViewSource.GetDefaultView(BeneficiariesCollection);
            Service = service;
        }
        #endregion

        #region Delegate Commands
        public DelegateCommand SaveCommand => new DelegateCommand(OnSaveCommand);
        public DelegateCommand AddRepresentantCommand => new DelegateCommand(OnAddRepresentantCommand);
        public DelegateCommand AddBeneficiaryCommand => new DelegateCommand(OnAddBeneficiaryCommand);
        public DelegateCommand EditBeneficiaryCommand => new DelegateCommand(OnEditBeneficiaryCommand);
        public DelegateCommand DeleteBeneficiaryCommand => new DelegateCommand(OnDeleteBeneficiaryCommand);
        public DelegateCommand CancelCommand => new DelegateCommand(OnCancelCommand);
        #endregion

        #region On Delegate Commands functions
        private void OnAddBeneficiaryCommand()
        {
            var person = Extensions.Wpf.Application.GetService<AddPersonDialog>()
                .ShowDialog(null, Properties.Resources.AddCaritasProject_Beneficiary_Title);
            BeneficiariesCollection.Add(person);
        }
        private void OnEditBeneficiaryCommand()
        {
            var selected= BeneficiariesCollection.SingleOrDefault(x => x.Id == ((Person)Beneficiaries.CurrentItem).Id);

            var edited = Extensions.Wpf.Application.GetService<AddPersonDialog>()
                .ShowDialog(selected, Properties.Resources.EditCaritasProject_Beneficiary_Title);
            if (edited == null || selected == null) return;

            selected.Assign(edited);
        }
        private void OnDeleteBeneficiaryCommand()
        {
            var currentBeneficiary = Beneficiaries.CurrentItem as Person;
            BeneficiariesCollection.Remove(currentBeneficiary);
        }
        private void OnAddRepresentantCommand()
        {
            var person = Extensions.Wpf.Application.GetService<AddPersonDialog>()
                .ShowDialog(null, Properties.Resources.AddCaritasProject_Representant_Title);
            Representant = person;
        }
        private void OnSaveCommand()
        {
            IsLoading = true;
            var bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
        }
        private void OnCancelCommand()
        {
            CloseAction();
        }
        #endregion

        #region Private functions

        private bool CheckProperties()
        {
            var properties = this.GetType().GetProperties();
            return properties.Select(item => this[item.Name]).All(validation => validation == string.Empty);
        }
        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
            CloseAction();
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            if (CheckProperties())
            {
                Dispatcher.Invoke(() =>
                {
                    DialogHostEx.ShowDialog(Application.Current.Windows.OfType<AddCaritasProjectDialog>().First(),
                        new LoadingMessage()
                        {
                            Message = "Guardando cambios en la Base de Datos...",
                        });
                });
                var newCaritasProject = new CaritasProject
                {
                    Id = EditionId,
                    Name = ProjectName,
                    Representant = Representant as Person,
                    Beneficiaries = BeneficiariesCollection.Cast<Person>()
                };
                if (IsEdition)
                {
                    Service.EditCaritasProject(newCaritasProject);
                }
                else
                {
                    Service.AddProject(newCaritasProject);
                }
                Dispatcher.Invoke(() =>
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    DialogHostEx.ShowDialog(Application.Current.Windows.OfType<AddCaritasProjectDialog>().First(),
                        new ValidationErrorMessage()
                        {
                            Message = "Hay campos que se encuentran sin llenar o con errores de formato. Corrija estos errores e intente nuevamente",
                        });

                });
            }
        }
        #endregion

        #region IDataErrorInfo implementation
        public string this[string columnName]
        {
            get
            {
                try
                {
                    var property = this.GetType().GetProperty(columnName);
                    var results = new List<ValidationResult>();
                    var result = property != null && Validator.TryValidateProperty(property.GetValue(this, null),
                        new ValidationContext(this, null, null)
                        {
                            MemberName = columnName
                        }, results);
                    return !result ? results.First().ErrorMessage : string.Empty;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
        }

        public string Error { get; }


        #endregion

        public void Edit(CaritasProject item)
        {
            IsEdition = true;
            EditionId = item.Id;
            ProjectName = item.Name;
            Representant = item.Representant;
            BeneficiariesCollection.AddRange(item.Beneficiaries);
        }
    }
}
