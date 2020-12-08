using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.Win32;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Entities;
using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Extensions.Wpf;
using OfficeEcclesial.App.Services;
using OfficeEcclesial.App.Views.Dialogs;
using OfficeEcclesial.Database.Entities;
using Prism.Commands;

namespace OfficeEcclesial.App.ViewModels.Sections
{
    public sealed class CaritasProjectsSectionViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<CaritasProject> _elementsCollection;
        private ICollectionView _elements;
        private bool _isLoading;
        private string _searchArgument;
        #endregion

        #region Constructor
        public CaritasProjectsSectionViewModel(CaritasProjectsService service, PdfService pdfService)
        {
            Service = service;
            PdfService = pdfService;
        }
        #endregion

        #region Bindable properties
        public ObservableCollection<CaritasProject> ElementsCollection
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
        public bool IsEmpty
        {
            get
            {
                if (IsLoading)
                    return false;
                return ElementsCollection?.Count == 0;
            }
        }
        #endregion

        #region Properties
        public CaritasProjectsService Service { get; }
        public PdfService PdfService { get; }
        #endregion

        #region Delegate Commands
        public DelegateCommand AddNewCommand => new DelegateCommand(OnAddNewCommand);
        public DelegateCommand<CaritasProject> SelectedItemCommand => new DelegateCommand<CaritasProject>(OnSelectedItemCommand);
        public DelegateCommand SearchCommand => new DelegateCommand(OnSearchCommand);
        public DelegateCommand RefreshCommand => new DelegateCommand(OnRefreshCommand);
        public DelegateCommand GeneratePdfCommand => new DelegateCommand(OnGeneratePdfCommand);
        public DelegateCommand<CaritasProject> EditElementCommand => new DelegateCommand<CaritasProject>(OnEditElementCommand);
        public DelegateCommand<CaritasProject> RemoveElementCommand => new DelegateCommand<CaritasProject>(OnRemoveElementCommand);
        #endregion

        #region On Delegate Command functions
        private void OnAddNewCommand()
        {
            Application.GetService<AddCaritasProjectDialog>().ShowDialog();
            InitializeComponent();
        }
        private void OnSelectedItemCommand(CaritasProject item)
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

                PdfService.GenerateCaritasProjectsList(ElementsCollection, saveFileDialog1.FileName);
                Process.Start(saveFileDialog1.FileName);
            };
            pdfWorker.RunWorkerAsync();

        }
        private void OnEditElementCommand(CaritasProject item)
        {
            Application.GetService<AddCaritasProjectDialog>().ShowDialog(item);
            InitializeComponent();
        }
        private void OnRemoveElementCommand(CaritasProject item)
        {
            Dispatcher.InvokeAsync(() =>
            {
                Service.RemoveProject(item.Id);
                InitializeComponent();
            });

        }
        #endregion

        public override void InitializeComponent()
        {
            IsLoading = true;
            ElementsCollection = new ObservableCollection<CaritasProject>();
            Elements = CollectionViewSource.GetDefaultView(ElementsCollection);
            Elements.Filter = OnElementsFilter;

            var bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerCompleted += BgOnRunWorkerCompleted;
            bg.RunWorkerAsync();
        }

        private bool OnElementsFilter(object obj)
        {
            if (string.IsNullOrEmpty(SearchArgument))
                return true;
            var searchArgument = SearchArgument.ToLower();
            return obj is ProjectItem item && (item.Name.ToLower().Contains(searchArgument));
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var persons = Service.GetAll();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Dispatcher.Invoke(() =>
                {
                    ElementsCollection.AddRange(persons);
                    IsLoading = false;
                });
            }
            catch (Exception exception)
            {
                e.Result = exception;
            }
        }

        private void BgOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (e.Result)
            {
                case Exception _:
                    break;
            }
        }
    }
}
