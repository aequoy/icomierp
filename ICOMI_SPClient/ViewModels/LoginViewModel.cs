using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Windows.Threading;
using ICOMI_SPClient.Utilities;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Message;
using System.Windows.Controls;
using ICOMI_DOMAIN;
using ICOMI_SPService;

namespace ICOMI_SPClient.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private readonly Dispatcher _dispatcher;
        #region Properties
        private string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string _passWord=string.Empty;
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }

        private string _errorText = string.Empty;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                RaisePropertyChanged("ErrorText");
            }
        }

        #endregion

        #region Ctor
        public LoginViewModel(Dispatcher disp)
        {
            this._dispatcher = disp;
            AnnulerCommand = new GenericCommand<object>(ExecuteAnnulerCommand, CanExecuteAnnulerCommand);
            ConnexionCommand = new GenericCommand<PasswordBox>(ExecuteConnexionCommand, CanExecuteConnexionCommand);
        }
        #endregion

        #region Commandes
        public ICommand AnnulerCommand
        {
            get;
            set;
        }

        public ICommand ConnexionCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteAnnulerCommand(object notused)
        {
            Messenger.Default.Send<ChangeStateMessage>(new ChangeStateMessage() { StateToChange = StateType.Accueil, StateFrom=StateType.Login });
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteAnnulerCommand(object notused)
        {
           return true;

        }

        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteConnexionCommand(PasswordBox pwdBox)
        {
            SessionContext securityContext = new SessionContext();
            GestionSharepoint gSP = new GestionSharepoint();
            var user = gSP.ConnectUser(UserName, pwdBox.Password);
            if (user != null)
            {
                securityContext.Utilisateur = user;
                //new User(){UserName="aequoy",ImgUrl="/ICOMI_SPClient;component/Resources/_alex.jpg"};
                Messenger.Default.Send<LoginMessage>(new LoginMessage() { SecurityContext = securityContext });
            }
            else
                ErrorText = "Impossible de se connecter";
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteConnexionCommand(PasswordBox pwdBox)
        {
           return !string.IsNullOrEmpty(UserName); 
        }
        #endregion
    }
}
