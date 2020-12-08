using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Data;
using Microsoft.Win32;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Extensions;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Extensions.Wpf;
using OfficeEcclesial.App.Services;
using OfficeEcclesial.App.Views.Dialogs;
using Prism.Commands;

namespace OfficeEcclesial.App.ViewModels.Sections
{
    public class PastoralPenitenciariaMembersSectionViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<PastoralPenitenciariaMembers> _elementsCollection;
        private ICollectionView _elements;
        private bool _isLoading;
        private string _searchArgument;
        #endregion

        #region Bindable properties
        public ObservableCollection<PastoralPenitenciariaMembers> ElementsCollection
        {
            get => _elementsCollection;
            set
            {
                _elementsCollection = value;
                RaisePropertyChanged(nameof(ElementsCollection));
            }
        }
        public ICollectionView Elements
        {
            get => _elements;
            set
            {
                _elements = value;
                RaisePropertyChanged(nameof(Elements));
            }
        }
        public bool IsEmpty
        {
            get
            {
                if (IsLoading)
                    return false;
                return ElementsCollection?.Count == 0;
            }
        }
        public string SearchArgument
        {
            get => _searchArgument;
            set
            {
                _searchArgument = value;
                RaisePropertyChanged(nameof(SearchArgument));
                Elements.Refresh();
            }
        }
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
                RaisePropertyChanged(nameof(IsEmpty));
            }
        }
        #endregion

        #region Properties
        public PastoralPenitenciariaMembersService Service { get; }
        public PdfService PdfService { get; }
        #endregion

        #region Constructor
        public PastoralPenitenciariaMembersSectionViewModel(PastoralPenitenciariaMembersService service, PdfService pdfService)
        {
            Service = service;
            PdfService = pdfService;
        }
        #endregion

        #region Delegate Commands
        public DelegateCommand AddNewCommand => new DelegateCommand(OnAddNewCommand);
        public DelegateCommand<PastoralPenitenciariaMembers> SelectedItemCommand => new DelegateCommand<PastoralPenitenciariaMembers>(OnSelectedItemCommand);
        public DelegateCommand SearchCommand => new DelegateCommand(OnSearchCommand);
        public DelegateCommand RefreshCommand => new DelegateCommand(OnRefreshCommand);
        public DelegateCommand GeneratePdfCommand => new DelegateCommand(OnGeneratePdfCommand);
        public DelegateCommand<PastoralPenitenciariaMembers> EditElementCommand => new DelegateCommand<PastoralPenitenciariaMembers>(OnEditElementCommand);
        public DelegateCommand<PastoralPenitenciariaMembers> RemoveElementCommand => new DelegateCommand<PastoralPenitenciariaMembers>(OnRemoveElementCommand);
        #endregion

        #region On Delegate Command functions
        private void OnAddNewCommand()
        {
            var person = Application.GetService<AddPersonDialog>().ShowDialog();
            if (person == null) return;
            Dispatcher.InvokeAsync(() =>
            {
                Service.AddMember(person.ToPastoralPenitenciariaMembers());
                InitializeComponent();
            });
        }
        private void OnEditElementCommand(PastoralPenitenciariaMembers item)
        {
            var person = Application.GetService<AddPersonDialog>().ShowDialog(item);
            if (person == null) return;
            Dispatcher.InvokeAsync(() =>
            {
                Service.EditMember(person.ToPastoralPenitenciariaMembers());

                InitializeComponent();
            });
        }
        private void OnRemoveElementCommand(PastoralPenitenciariaMembers item)
        {
            Dispatcher.InvokeAsync(() =>
            {
                Service.RemoveMember(item.Id);
                InitializeComponent();
            });

        }
        private void OnSelectedItemCommand(PastoralPenitenciariaMembers item)
        {
            foreach (var personItem in ElementsCollection)
            {
                personItem.IsSelected = false;
            }

            item.IsSelected = true;
        }

        private void OnRefreshCommand()
        {
            SearchArgument = string.Empty;
            InitializeComponent();
        }
        private void OnSearchCommand()
        {
            Elements.Refresh();
        }
        private void OnGeneratePdfCommand()
        {
            var pdfWorker = new BackgroundWorker();
            pdfWorker.DoWork += (sender, args) =>
            {
                var saveFileDialog1 = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    ValidateNames = true
                };

                var result = saveFileDialog1.ShowDialog();
                if (result == null || !result.Value) return;

                PdfService.GeneratePastoralPenitenciariaMembersList(ElementsCollection, saveFileDialog1.FileName);
                Process.Start(saveFileDialog1.FileName);
            };
            pdfWorker.RunWorkerAsync();

        }

        #endregion

        public override void InitializeComponent()
        {
            IsLoading = true;

            ElementsCollection = new ObservableCollection<PastoralPenitenciariaMembers>();

            Elements = CollectionViewSource.GetDefaultView(ElementsCollection);
            Elements.Filter = OnElementsFilter;

            var bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerAsync();
        }

        private bool OnElementsFilter(object obj)
        {
            if (string.IsNullOrEmpty(SearchArgument))
                return true;
            var searchArgument = SearchArgument.ToLower();
            return obj is Person item && (item.Name.ToLower().Contains(searchArgument) || item.LastName.ToLower().Contains(searchArgument));
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var members = Service.GetAll();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Dispatcher.Invoke(() =>
                {
                    ElementsCollection.AddRange(members);
                    IsLoading = false;
                });
            }
            catch (Exception exception)
            {
                e.Result = exception;
            }
        }
    }
}
