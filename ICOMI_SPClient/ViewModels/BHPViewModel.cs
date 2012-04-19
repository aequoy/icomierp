using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using ICOMI_CONTENT;
using GalaSoft.MvvmLight;
using ICOMI_SPService;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Message;
using ICOMI_SPClient.Utilities;

namespace ICOMI_SPClient.ViewModels
{
    public class BHPViewModel : ViewModelBase
    {
        private readonly Dispatcher _dispatcher;
        
        public ObservableCollection<Rubrique> LesRubriques { get; private set; }
        public BHPViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            LesRubriques = new ObservableCollection<Rubrique>(RefObjectSPService.GetRubriques());
            SelectRubriqueCommand = new GenericCommand<Rubrique>(ExecuteSelectRubriqueCommand, CanExecuteSelectRubriqueCommand);
        }



        #region Command
        public ICommand SelectRubriqueCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteSelectRubriqueCommand(Rubrique param)
        {
            Messenger.Default.Send<SelectRubriqueMessage>(new SelectRubriqueMessage() { RubriqueSelected =param});
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteSelectRubriqueCommand(Rubrique param)
        {
            return true;
        }
        #endregion
    }
}
