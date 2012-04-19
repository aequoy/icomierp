using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using ICOMI_DOMAIN;
using System.Windows.Threading;
using System.Windows.Input;
using ICOMI_SPClient.Utilities;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Message;

namespace ICOMI_SPClient.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly Dispatcher _dispatcher;
        #region Properties
        private IcomiTask _LaTache = null;
        public IcomiTask LaTache
        {
            get { return this._LaTache; }
            set
            {
                this._LaTache = value;
                RaisePropertyChanged("LaTache");
            }
        }
        
        #endregion

        #region Ctor
        public TaskViewModel(Dispatcher disp)
        {
            this._dispatcher = disp;
            AnnulerCommand = new GenericCommand<object>(ExecuteAnnulerCommand, CanExecuteAnnulerCommand);
            EffectuerCommand = new GenericCommand<object>(ExecuteEffectuerCommand, CanExecuteEffectuerCommand);
            DeclarerProbleme = new GenericCommand<object>(ExecuteDeclarerProblemeCommand, CanExecuteDeclarerProblemeCommand);
        }
        #endregion

        #region Commandes
        public ICommand AnnulerCommand
        {
            get;
            set;
        }

        public ICommand EffectuerCommand
        {
            get;
            set;
        }

        public ICommand DeclarerProbleme
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
            Messenger.Default.Send<ChangeStateMessage>(new ChangeStateMessage() { StateToChange = StateType.Accueil, StateFrom = StateType.MyTasks });
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
        protected virtual void ExecuteEffectuerCommand(object notused)
        {
            Messenger.Default.Send<ChangeStateMessage>(new ChangeStateMessage() { StateToChange = StateType.Accueil, StateFrom = StateType.MyTasks });
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteEffectuerCommand(object notused)
        {
            return true;
        }


        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteDeclarerProblemeCommand(object notused)
        {
            Messenger.Default.Send<ChangeStateMessage>(new ChangeStateMessage() { StateToChange = StateType.Accueil, StateFrom = StateType.MyTasks });
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteDeclarerProblemeCommand(object notused)
        {
            return true;
        }
        #endregion
    }
}
