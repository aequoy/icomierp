using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using ICOMI_SPClient.Views;
using System.Windows.Controls;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Message;
using System.Windows.Input;
using ICOMI_SPClient.Utilities;
using System.Windows;
using ICOMI_SPClient.Utilities.Animation;
using ICOMI_DOMAIN;

namespace ICOMI_SPClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region attribute
        /// <summary>
        /// The default homePage.
        /// </summary>
        //private Lazy<MenuView> accueilPage = new Lazy<MenuView>(() => new MenuView());
        private Lazy<AccueilView> accueilPage = new Lazy<AccueilView>(() => new AccueilView());

        /// <summary>
        /// The menu homePage.
        /// </summary>
        private Lazy<MenuView> menuPage = new Lazy<MenuView>(() => new MenuView());
        
        /// <summary>
        /// The personal homePage.
        /// </summary>
        private Lazy<HomeView> homePage = new Lazy<HomeView>(() => new HomeView());

        /// <summary>
        /// The corePage.
        /// </summary>
        private Lazy<BHPView> bhpPage = new Lazy<BHPView>(() => new BHPView());

        /// <summary>
        /// The sdkPage.
        /// </summary>
        private Lazy<HACCPView> haccpPage = new Lazy<HACCPView>(() => new HACCPView());

        /// <summary>
        /// The divers Page.
        /// </summary>
        private Lazy<DiversView> diversPage = new Lazy<DiversView>(() => new DiversView());

        /// <summary>
        /// The add document page.
        /// </summary>
        private Lazy<AddBHPDocumentView> addBHPPage = new Lazy<AddBHPDocumentView>(() => new AddBHPDocumentView());

        /// <summary>
        /// The login Page.
        /// </summary>
        private Lazy<LoginView> loginPage = new Lazy<LoginView>(() => new LoginView());

        /// <summary>
        /// The view task Page.
        /// </summary>
        private Lazy<TaskView> viewTaskPage = new Lazy<TaskView>(() => new TaskView());

        /// <summary>
        /// The search Page.
        /// </summary>
        private Lazy<SearchView> searchPage = new Lazy<SearchView>(() => new SearchView());
        
        /// <summary>
        /// The recette Page.
        /// </summary>
        private Lazy<RecetteView> recettePage = new Lazy<RecetteView>(() => new RecetteView());

        /// <summary>
        /// The DocumentRubrique Page.
        /// </summary>
        private Lazy<BHPDocumentView> bphDocumentRubriquePage = new Lazy<BHPDocumentView>(() => new BHPDocumentView());

        /// <summary>
        /// The Etablissement Page.
        /// </summary>
        private Lazy<EtablissementView> etablissementPage = new Lazy<EtablissementView>(() => new EtablissementView());

        /// <summary>
        /// The Installation Page.
        /// </summary>
        private Lazy<InstallationView> installationPage = new Lazy<InstallationView>(() => new InstallationView());


        private readonly Dispatcher _dispatcher;
        private StateType ActiveState;

        #endregion

        #region Properties

        private string _PageTitle = "Carnet Sanitaire";
        public string PageTitle
        {
            get { return this._PageTitle; }
            set
            {
                this._PageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        private object _activeView = null;
        public object ActiveView
        {
            get { return this._activeView; }
            set
            {
                this._activeView = value;
                RaisePropertyChanged("ActiveView");
            }
        }

        private object _activePopup = null;
        public object ActivePopup
        {
            get { return this._activePopup; }
            set
            {
                this._activePopup = value;
                RaisePropertyChanged("ActivePopup");
            }
        }

        private Visibility _activePopupVisibility = Visibility.Hidden;
        public Visibility ActivePopupVisibility
        {
            get { return this._activePopupVisibility; }
            set
            {
                this._activePopupVisibility = value;
                RaisePropertyChanged("ActivePopupVisibility");
            }
        }

        private bool _IsLogged = false;
        public bool IsLogged
        {
            get { return this._IsLogged; }
            set
            {
                this._IsLogged = value;
                RaisePropertyChanged("IsLogged");
            }
        }

        private User _User = null;
        public User User
        {
            get { return this._User; }
            set
            {
                this._User = value;
                RaisePropertyChanged("User");
            }
        }

        #endregion

        #region Ctor
        public MainViewModel(Dispatcher dispatcher)
        {
            this._dispatcher = dispatcher;
            //this.ActiveView = this.accueilPage.Value;
            PageTitle = "Mes installations";
            this.ActiveView = this.installationPage.Value;
            ActiveState = StateType.Installation;
            RegisterToMessages();
            AccueilCommand = new GenericCommand<object>(ExecuteAccueilCommand, CanExecuteAccueilCommand);
            LoginCommand = new GenericCommand<object>(ExecuteLoginCommand, CanExecuteLoginCommand);
            DisconnectCommand = new GenericCommand<object>(ExecuteDisconnectCommand, CanExecuteDisconnectCommand);
        }
        #endregion

        #region command
        public ICommand AccueilCommand
        {
            get;
            set;
        }

        public ICommand LoginCommand
        {
            get;
            set;
        }
        public ICommand DisconnectCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteDisconnectCommand(object notused)
        {
            this.IsLogged = false;
            PageTitle = "Carnet Sanitaire";
            this.ActiveView = this.accueilPage.Value;
            ActiveState = StateType.Accueil;
            InitMonEspace();
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteDisconnectCommand(object notused)
        {
                return true;
        }
        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteAccueilCommand(object notused)
        {
            PageTitle = "Carnet Sanitaire";
            this.ActiveView = this.accueilPage.Value;
            ActiveState = StateType.Accueil;
            InitMonEspace();
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteAccueilCommand(object notused)
        {
            if(ActiveState == StateType.Accueil)
                return false;
            else
                return true;
            
        }

        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteLoginCommand(object notused)
        {
            this.ActivePopup = this.loginPage.Value;
            ActiveState = StateType.Login;
            (this.ActivePopup as UIElement).Opacity = 0d;
            this.ActivePopupVisibility = Visibility.Visible;            
            WPFAnimationHelper.AnimateEasingEquation(this.ActivePopup as DependencyObject, UIElement.OpacityProperty, EasingFunction.Linear, 1, 500);
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteLoginCommand(object notused)
        {
                return true;
        }
        #endregion

        #region Methods
        private void RegisterToMessages()
        {
            RegisterToChangeStateMessage();
            RegisterToSessionContextMessage();
            RegisterToTaskMessage();
            RegisterToAddBPHMessage();
            RegisterToSelectRubriqueMessage();
        }


        private void RegisterToSelectRubriqueMessage()
        {
            Messenger.Default.Register<SelectRubriqueMessage>(this, (e) =>
            {
                PageTitle = string.Empty;
                this.ActivePopupVisibility = Visibility.Hidden;
                this.ActiveView = this.bphDocumentRubriquePage.Value;
                (this.bphDocumentRubriquePage.Value.DataContext as BHPDocumentViewModel).LaRubrique = e.RubriqueSelected;
                this.ActiveState = StateType.BHP;
            });
        }

        private void RegisterToAddBPHMessage()
        {
            Messenger.Default.Register<AddBPHMessage>(this, (e) =>
            {
                PageTitle = string.Empty;
                this.ActivePopupVisibility = Visibility.Hidden;
                this.ActiveView = this.addBHPPage.Value;                
                this.ActiveState = StateType.AddBHPDocument;     
            });
        }

        private void RegisterToTaskMessage()
        {
            Messenger.Default.Register<ViewTaskMessage>(this, (e) =>
                {
                    this.ActivePopup = this.viewTaskPage.Value;
                    (this.viewTaskPage.Value.DataContext as TaskViewModel).LaTache = e.TaskToShow;
                    ActiveState = StateType.MyTasks;
                    (this.ActivePopup as UIElement).Opacity = 0d;
                    this.ActivePopupVisibility = Visibility.Visible;
                    WPFAnimationHelper.AnimateEasingEquation(this.ActivePopup as DependencyObject, UIElement.OpacityProperty, EasingFunction.Linear, 1, 500);
                });
        }

        private void RegisterToSessionContextMessage()
        {
            Messenger.Default.Register<LoginMessage>(this, (e) =>
                {
                    this.IsLogged = true;
                    this.User = e.SecurityContext.Utilisateur;
                    WPFAnimationHelper.AnimateEasingEquation(this.ActivePopup as DependencyObject, UIElement.OpacityProperty, EasingFunction.Linear, 0, 500, delegate
                    {
                        PageTitle = "Mon espace";
                        this.ActivePopupVisibility = Visibility.Hidden;
                        this.ActiveView = this.homePage.Value;
                        (this.homePage.Value.DataContext as HomeViewModel).UserConnected = this.User;
                        this.ActiveState = StateType.Home;                      
                    });
                });
        }
        private void RegisterToChangeStateMessage()
        {
            Messenger.Default.Register<ChangeStateMessage>(this, (e) =>
            {
                if (e.StateFrom == StateType.Login)
                {
                    WPFAnimationHelper.AnimateEasingEquation(this.ActivePopup as DependencyObject, UIElement.OpacityProperty, EasingFunction.Linear, 0, 500, delegate
                    {
                        this.ActivePopupVisibility = Visibility.Hidden;
                    });

                }
                if (e.StateFrom == StateType.MyTasks)
                {
                    WPFAnimationHelper.AnimateEasingEquation(this.ActivePopup as DependencyObject, UIElement.OpacityProperty, EasingFunction.Linear, 0, 500, delegate
                    {
                        this.ActivePopupVisibility = Visibility.Hidden;
                    });

                }
                switch (e.StateToChange)
                {
                    case StateType.Accueil:
                        PageTitle = "Carnet Sanitaire";
                        this.ActiveView = this.accueilPage.Value;                       
                        break;
                    case StateType.Recettes:
                        PageTitle = "Recettes";
                        this.ActiveView = this.recettePage.Value;
                        break;
                    case StateType.Etablissement:
                        PageTitle = "Mon établissement";
                        this.ActiveView = this.etablissementPage.Value;
                        break;
                    case StateType.Installation:
                        PageTitle = "Mes installations";
                        this.ActiveView = this.installationPage.Value;
                        break;
                    case StateType.Menu:
                        PageTitle = "Mon espace";
                        this.ActiveView = this.menuPage.Value;
                        InitMonEspace();
                        break;
                    case StateType.Search:
                        PageTitle = "Recherche de documents";
                        this.ActiveView = this.searchPage.Value;
                        break;
                    case StateType.Home:
                        PageTitle = "Mon espace";
                        this.ActiveView = this.homePage.Value;
                        (this.homePage.Value.DataContext as HomeViewModel).UserConnected = this.User;
                        break;
                    case StateType.BHP:
                        PageTitle = "Bonnes pratiques d'hygiène";
                        this.ActiveView = this.bhpPage.Value;
                        break;
                    case StateType.AddBHPDocument:
                        PageTitle = "Ajouter un document dans les BHP";
                        this.ActiveView = this.addBHPPage.Value;
                        break;
                    case StateType.Login:
                        this.ActivePopup = this.loginPage.Value;
                        (this.ActivePopup as UIElement).Opacity = 0d;
                        this.ActivePopupVisibility = Visibility.Visible;
                        WPFAnimationHelper.AnimateEasingEquation(this.ActivePopup as DependencyObject, UIElement.OpacityProperty, EasingFunction.Linear, 1, 500);

                        break;
                }
                ActiveState = e.StateToChange;

            });
        }

        private void InitMonEspace()
        {
            var vDt = this.menuPage.Value.DataContext as MenuViewModel;
            if (vDt != null)
            {
                if(vDt.IsConnected != this.IsLogged)
                    vDt.IsConnected = this.IsLogged;
            }
        }
        #endregion
    }
    
}
