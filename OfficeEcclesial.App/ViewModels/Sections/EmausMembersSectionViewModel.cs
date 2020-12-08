using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Data;
using Microsoft.Win32;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Extensions.Wpf;
using OfficeEcclesial.App.Services;
using OfficeEcclesial.App.Views.Dialogs;
using Prism.Commands;
using OfficeEcclesial.App.Extensions;

namespace OfficeEcclesial.App.ViewModels.Sections
{
    public class EmausMembersSectionViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<EmausMembers> _elementsCollection;
        private ICollectionView _elements;
        private bool _isLoading;
        private string _searchArgument;
        #endregion

        #region Properties
        public ObservableCollection<EmausMembers> ElementsCollection
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
        private EmausMembersService Service { get; }
        private PdfService PdfService { get; }
        #endregion

        #region Constructor
        public EmausMembersSectionViewModel(EmausMembersService service, PdfService pdfService)
        {
            Service = service;
            PdfService = pdfService;
        }
        #endregion

        #region Delegate Commands
        public DelegateCommand AddNewCommand => new DelegateCommand(OnAddNewCommand);
        public DelegateCommand<EmausMembers> SelectedItemCommand => new DelegateCommand<EmausMembers>(OnSelectedItemCommand);
        public DelegateCommand SearchCommand => new DelegateCommand(OnSearchCommand);
        public DelegateCommand RefreshCommand => new DelegateCommand(OnRefreshCommand);
        public DelegateCommand GeneratePdfCommand => new DelegateCommand(OnGeneratePdfCommand);
        public DelegateCommand<EmausMembers> EditElementCommand => new DelegateCommand<EmausMembers>(OnEditElementCommand);
        public DelegateCommand<EmausMembers> RemoveElementCommand => new DelegateCommand<EmausMembers>(OnRemoveElementCommand);
        #endregion

        #region On Delegate Command functions
        private void OnAddNewCommand()
        {
            var person = Application.GetService<AddPersonDialog>().ShowDialog();
            if (person == null) return;
            Dispatcher.InvokeAsync(() =>
            {
                Service.Add(person.ToEmausMembers());
                InitializeComponent();
            });
        }
        private void OnEditElementCommand(EmausMembers item)
        {
            var person = Application.GetService<AddPersonDialog>().ShowDialog(item);
            if (person == null) return;
            Dispatcher.InvokeAsync(() =>
            {
                Service.Edit(person.ToEmausMembers());

                InitializeComponent();
            });
        }
        private void OnRemoveElementCommand(EmausMembers item)
        {
            Dispatcher.InvokeAsync(() =>
            {
                Service.Remove(item.Id);
                InitializeComponent();
            });

        }
        private void OnSelectedItemCommand(EmausMembers item)
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

                PdfService.GenerateEmausMembersList(ElementsCollection, saveFileDialog1.FileName);
                Process.Start(saveFileDialog1.FileName);
            };
            pdfWorker.RunWorkerAsync();
        }

        #endregion

        public override void InitializeComponent()
        {
            IsLoading = true;

            ElementsCollection = new ObservableCollection<EmausMembers>();

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
