using OfficeEcclesial.App.Extensions.Mvvm;
using OfficeEcclesial.App.Extensions.Wpf;
using OfficeEcclesial.App.Views.Sections;
using OfficeEcclesial.App.Views.Toolbars;
using OfficeEcclesial.App.Views.Windows;

using Prism.Commands;

using System.Windows.Controls;

namespace OfficeEcclesial.App.ViewModels
{
    public sealed class MainWindowViewModel : BindableBase
    {
        #region Fields

        private bool _openPrincipalMenu;
        

        #endregion

        #region Properties

        public bool OpenPrincipalMenu
        {
            get => _openPrincipalMenu;
            set
            {
                _openPrincipalMenu = value;
                RaisePropertyChanged(nameof(OpenPrincipalMenu));
            }
        }
        private object _currentToolBar;

        public object CurrentToolBar
        {
            get { return _currentToolBar; }
            set
            {
                _currentToolBar = value;
                RaisePropertyChanged(nameof(CurrentToolBar));
            }
        }
        private string _currentContentTitle;

        public string CurrentContentTitle
        {
            get { return _currentContentTitle; }
            set
            {
                _currentContentTitle = value;
                RaisePropertyChanged(nameof(CurrentContentTitle));
            }
        }

        private UserControl _currentContent;
        public UserControl CurrentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                RaisePropertyChanged(nameof(CurrentContent));
            }
        }


        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            ShowCatequesisMembersCommand.Execute();
        }
        #endregion

        public DelegateCommand ShowCaritasMembersCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is CaritasMembersSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Cáritas: Miembros";
            CurrentContent = Application.GetService<CaritasMembersSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<CaritasMembersSectionToolbar>());

        });
        public DelegateCommand ShowCaritasProjectsCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is CaritasProjectsSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Cáritas: Proyectos";
            CurrentContent = Application.GetService<CaritasProjectsSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<CaritasProjectsSectionToolbar>());
        });
        public DelegateCommand ShowPastoralPenitenciariaMembersCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is PastoralPenitenciariaMembersSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Pastoral penitenciaria: Miembros";
            CurrentContent = Application.GetService<PastoralPenitenciariaMembersSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<PastoralPenitenciariaMembersToolbar>());
        });
        public DelegateCommand ShowPastoralPenitenciariaBeneficiariesCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is PastoralPenitenciariaBeneficiariesSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Pastoral penitenciaria: Beneficiarios";
            CurrentContent = Application.GetService<PastoralPenitenciariaBeneficiariesSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<PastoralPenitenciariaMembersToolbar>());
        });
        public DelegateCommand ShowLiturgiaCantoresCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is LiturgiaCantoresSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Liturgia: Cantores";
            CurrentContent = Application.GetService<LiturgiaCantoresSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<LiturgiaCantoresToolbar>());
        });
        public DelegateCommand ShowLiturgiaLectoresCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is LiturgiaLectoresSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Liturgia: Lectores";
            CurrentContent = Application.GetService<LiturgiaLectoresSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<LiturgiaLectoresToolbar>());
        });
        public DelegateCommand ShowLiturgiaMembersCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is LiturgiaMembersSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Liturgia: Miebros";
            CurrentContent = Application.GetService<LiturgiaMembersSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<LiturgiaMembersToolbar>());
        });
        public DelegateCommand ShowLiturgiaMonaguillosCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is LiturgiaMonaguillosSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Liturgia: Monaguillos";
            CurrentContent = Application.GetService<LiturgiaMonaguillosSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<LiturgiaMonaguillosToolbar>());
        });
        public DelegateCommand ShowConsejoParroquialMembersCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is ConsejoParroquialMembersSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Consejo Parroquial: Miembros";
            CurrentContent = Application.GetService<ConsejoParroquialMembersSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<ConsejoParroquialMembersToolbar>());
        });
        public DelegateCommand ShowEmausMembersCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is EmausMembersSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Emaus: Miembros";
            CurrentContent = Application.GetService<EmausMembersSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<EmausMembersToolbar>());
        });
        public DelegateCommand ShowSettingsCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is SettingsSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Configuración";
            CurrentContent = Application.GetService<SettingsSectionView>();
            CurrentToolBar = null;
        });       
        public DelegateCommand ShowCatequesisCatequistasCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is CatequesisCatequistasSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Catequesis: Catequistas";
            CurrentContent = Application.GetService<CatequesisCatequistasSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<CatequesisCatequistasSectionToolbar>());
        });
        public DelegateCommand ShowCatequesisMembersCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is CatequesisMembersSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Catequesis: Miembros";
            CurrentContent = Application.GetService<CatequesisMembersSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<CatequesisMembersSectionToolbar>());
        });
        public DelegateCommand ShowAdultosCatequistasCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is CatequistasAdultosSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Catequesis de adultos: Catequistas";
            CurrentContent = Application.GetService<CatequistasAdultosSectionView>();
            CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<CatequistasAdultosSectionToolbar>());
        });
        public DelegateCommand ShowHomeCommand => new DelegateCommand(() =>
        {
            if (CurrentContent is HomeSectionView) return;
            OpenPrincipalMenu = false;
            CurrentContentTitle = "Bienvenido";
            CurrentContent = Application.GetService<HomeSectionView>();
            //CurrentToolBar = CurrentContent.BindToolbar(Application.GetService<CaritasProjectsSectionToolbar>());
        });
        public DelegateCommand ShowAboutOfCommand => new DelegateCommand(() =>
        {
            OpenPrincipalMenu = false;
            Application.GetService<AboutOfWindow>().ShowDialog();
        });
    }
}
