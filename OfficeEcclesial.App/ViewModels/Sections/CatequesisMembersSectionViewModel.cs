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
    public class CatequesisMembersSectionViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<CatequesisMember> _elementsCollection;
        private ICollectionView _elements;
        private bool _isLoading;
        private string _searchArgument;
        #endregion

        #region Properties
        public ObservableCollection<CatequesisMember> ElementsCollection
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
        private CatequesisMembersService Service { get; }
        private PdfService PdfService { get; }
        #endregion

        #region Constructor
        public CatequesisMembersSectionViewModel(CatequesisMembersService service, PdfService pdfService)
        {
            Service = service;
            PdfService = pdfService;
        }
        #endregion

        #region Delegate Commands
        public DelegateCommand AddNewCommand => new DelegateCommand(OnAddNewCommand);
        public DelegateCommand<CatequesisMember> SelectedItemCommand => new DelegateCommand<CatequesisMember>(OnSelectedItemCommand);
        public DelegateCommand SearchCommand => new DelegateCommand(OnSearchCommand);
        public DelegateCommand RefreshCommand => new DelegateCommand(OnRefreshCommand);
        public DelegateCommand GeneratePdfCommand => new DelegateCommand(OnGeneratePdfCommand);
        public DelegateCommand<CatequesisMember> EditElementCommand => new DelegateCommand<CatequesisMember>(OnEditElementCommand);
        public DelegateCommand<CatequesisMember> RemoveElementCommand => new DelegateCommand<CatequesisMember>(OnRemoveElementCommand);
        #endregion

        #region On Delegate Command functions
        private void OnAddNewCommand()
        {
            var person = Application.GetService<AddCatequesisMemberDialog>().ShowDialog();
            if (person == null) return;
            Dispatcher.InvokeAsync(() =>
            {
                Service.Add(person);
                InitializeComponent();
            });
        }
        private void OnEditElementCommand(CatequesisMember item)
        {
            var person = Application.GetService<AddCatequesisMemberDialog>().ShowDialog(item);
            if (person == null) return;
            Dispatcher.InvokeAsync(() =>
            {
                Service.Edit(person);

                InitializeComponent();
            });
        }
        private void OnRemoveElementCommand(CatequesisMember item)
        {
            Dispatcher.InvokeAsync(() =>
            {
                Service.Remove(item.Id);
                InitializeComponent();
            });

        }
        private void OnSelectedItemCommand(CatequesisMember item)
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

                PdfService.GenerateCatequesisMembersList(ElementsCollection, saveFileDialog1.FileName);
                Process.Start(saveFileDialog1.FileName);
            };
            pdfWorker.RunWorkerAsync();

        }

        #endregion

        public override void InitializeComponent()
        {
            IsLoading = true;

            ElementsCollection = new ObservableCollection<CatequesisMember>();

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
