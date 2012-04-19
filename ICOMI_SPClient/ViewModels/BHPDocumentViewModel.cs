using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using ICOMI_CONTENT;
using ICOMI_DOMAIN;
using ICOMI_SPService;
using System.Windows.Input;
using ICOMI_SPClient.Utilities;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace ICOMI_SPClient.ViewModels
{
    public class BHPDocumentViewModel:ViewModelBase
    {
          
         private readonly Dispatcher _dispatcher;

         private ObservableCollection<DocumentMetadataDataContract> _LesDocuments = null;
         public ObservableCollection<DocumentMetadataDataContract> LesDocuments
         {
             get { return _LesDocuments; }
             set
             {
                 this._LesDocuments = value;
                 RaisePropertyChanged("LesDocuments");
             }
         }

        private DocumentMetadataDataContract _SelectedDocument;
        public DocumentMetadataDataContract SelectedDocument { 
            get { return _SelectedDocument; }
            set { 
                this._SelectedDocument = value;
                RaisePropertyChanged("SelectedDocument");
            } 
        }

        private Rubrique _LaRubrique = null;
        public Rubrique LaRubrique
        {
            get { return this._LaRubrique; }
            set
            {
                this._LaRubrique = value;
                //Retrieve des documents en fonction de la rubrique
                LesDocuments = RetrieveDocumentsForRubrique(LaRubrique);
                if (LesDocuments != null && LesDocuments.Count > 0)
                    this.SelectedDocument = LesDocuments[0];
                RaisePropertyChanged("LaRubrique");
            }
        }


        #region CTOR
        /// <summary>
        /// Le constructeur
        /// </summary>
        /// <param name="dispatcher">Le paramètre</param>
        public BHPDocumentViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            ChangeDocumentCommand = new GenericCommand<DocumentMetadataDataContract>(ExecuteValiderCommand, CanExecuteValiderCommand);
        }
        #endregion

        #region Command
        /// <summary>
        /// 
        /// </summary>
        public ICommand ChangeDocumentCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteValiderCommand(DocumentMetadataDataContract param)
        {
            if (param != null)
            {
                this.SelectedDocument = param;
            }
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteValiderCommand(DocumentMetadataDataContract param)
        {
            return true;
        }
        #endregion
        #region methods
        private ObservableCollection<DocumentMetadataDataContract> RetrieveDocumentsForRubrique(Rubrique laRubrique)
        {
            try
            {
                var gSP = new GestionSharepoint();
                string LibraryName = "ICOMI_BPH";
                string extension = string.Empty;
                var l = gSP.GetAllDocumentMetaByRubrique(laRubrique.Code, LibraryName, out extension);
                if (l != null)
                {
                    return new ObservableCollection<DocumentMetadataDataContract>(l);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
            return null;
        }
        #endregion
    }
}
