using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using ICOMI_DOMAIN;
using System.ComponentModel;
using System.Windows.Input;
using ICOMI_SPClient.Utilities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Message;
using ICOMI_SPService;
using System.IO;
namespace ICOMI_SPClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
         private readonly Dispatcher _dispatcher;
         #region Properties
         public ObservableCollection<DocumentMetadataDataContract> LesNouveauxDocuments { get; private set; }

         private ObservableCollection<IcomiTask> _LesTachesToDo;
         
        public ObservableCollection<IcomiTask> LesTachesToDo
         {
             get { return _LesTachesToDo; }
             private set
             {
                 this._LesTachesToDo = value;
                 RaisePropertyChanged("LesTachesToDo");
             }
         }
                
         private int _SelectedIndex=0;
         public int SelectedIndex
         {
             get { return _SelectedIndex; }
             set
             {
                 this._SelectedIndex = value;
                 RaisePropertyChanged("SelectedIndex");
             }
         }

         private User _UserConnected = null;
         public User UserConnected
         {
             get { return _UserConnected; }
             set
             {
                 this._UserConnected = value;
                 
                 GestionSharepoint gSP = new GestionSharepoint();
                 LesTachesToDo = new ObservableCollection<IcomiTask>(gSP.GetTodaysTasksForUser(UserConnected.NomComplet));
                 RaisePropertyChanged("UserConnected");
                 RaisePropertyChanged("LesTachesToDo");
             }
         }
        #endregion

         #region CTOR
         public HomeViewModel(Dispatcher dispatcher)
         {
             _dispatcher = dispatcher;
             
             ChangeSelectedIndexCommand = new GenericCommand<string>(ExecuteChangeSelectedCommand, CanExecuteChangeSelectedCommand);
             SelectTaskCommand = new GenericCommand<IcomiTask>(ExecuteSelectTaskCommand, CanExecuteSelectTaskCommand);
             AddBPHCommand = new GenericCommand<IcomiTask>(ExecuteAddBPHCommand, CanExecuteAddBPHCommand);
             ViewDocumentCommand = new GenericCommand<DocumentMetadataDataContract>(ExecuteViewDocumentCommand, CanExecuteViewDocumentCommand);
             GestionSharepoint gSP = new GestionSharepoint();
             LesNouveauxDocuments = new ObservableCollection<DocumentMetadataDataContract>(gSP.GetAllChangedDocumentMetaByRubrique("ICOMI_BPH"));
             #region toDelete
             //LesNouveauxDocuments = new ObservableCollection<DocumentMetadataDataContract>();
             //for (int i = 0; i < 20; i++)
             //{
             //    var dmDC = new DocumentMetadataDataContract();
             //       dmDC.Auteur = "Alexandre Equoy";                    
             //       dmDC.Commentaires="Je peux laisser un commentaire sur ce document";
             //       dmDC.CreateDate = DateTime.Now;
             //       dmDC.DocumentVersion=1;                    
             //       dmDC.Producteur = "ACCESS IT";
             //       dmDC.RubriqueDocument = "Lutte contre les nuisibles";
             //       dmDC.SousTypeDocument = "Preuve";                    
             //       dmDC.TypeDocument = "Contrat";
             //       dmDC.RubriqueDocumentCode = "LN";
             //       dmDC.SousTypeDocumentCode = "PRV";
             //       dmDC.TypeDocumentCode = "CTN";
             //       dmDC.Titre = "Catalogue de formation";
             //       LesNouveauxDocuments.Add(dmDC);

             //this.SelectedIndex = LesNouveauxDocuments.Count/2;
             
             //if (UserConnected != null)
             //{
                
             //}
             //else
             //{
             //    LesTachesToDo = new ObservableCollection<IcomiTask>();
             //    for (int i = 0; i < 4; i++)
             //    {
             //        LesTachesToDo.Add(new IcomiTask() { Title = "Prise de température dans les douches", TileTitle = "Prise de T°", NbOccurence = 1 });
             //    }
             //}
             #endregion

         }
        #endregion

         #region Command
         public ICommand ChangeSelectedIndexCommand
         {
             get;
             set;
         }

         public ICommand SelectTaskCommand
         {
             get;
             set;
         }

         public ICommand AddBPHCommand
         {
             get;
             set;
         }

         public ICommand ViewDocumentCommand
         {
             get;
             set;
         }

         /// <summary>
         /// Valide l'édition en cours et retourne à l'écran principal
         /// </summary>
         /// <param name="notUsed"></param>
         protected virtual void ExecuteViewDocumentCommand(DocumentMetadataDataContract param)
         {
             try
             {
                 var leDocument = param.NomDocument;

                 string savedFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), leDocument);
                 if (!File.Exists(savedFile))
                 {
                     _dispatcher.Invoke(
                            System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(
                              delegate()
                              {
                                  GestionSharepoint gSP = new GestionSharepoint();
                                  string extension = string.Empty;
                                  var document = gSP.GetFile(leDocument, "ICOMI_BPH", out extension);
                                  File.WriteAllBytes(savedFile, document);
                              }
                          ));
                 }
                 System.Diagnostics.Process.Start(savedFile);
             }
             catch
             {

             }
             
         }

         /// <summary>
         /// Indique si l'objet est valide
         /// </summary>
         private bool CanExecuteViewDocumentCommand(DocumentMetadataDataContract param)
         {
             return true;
         }

         /// <summary>
         /// Valide l'édition en cours et retourne à l'écran principal
         /// </summary>
         /// <param name="notUsed"></param>
         protected virtual void ExecuteAddBPHCommand(object param)
         {
             Messenger.Default.Send<AddBPHMessage>(new AddBPHMessage());
         }

         /// <summary>
         /// Indique si l'objet est valide
         /// </summary>
         private bool CanExecuteAddBPHCommand(object param)
         {
             return true;
         }

         /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
         protected virtual void ExecuteSelectTaskCommand(IcomiTask param)
         {
             Messenger.Default.Send<ViewTaskMessage>(new ViewTaskMessage() { TaskToShow = param });
         }

         /// <summary>
         /// Indique si l'objet est valide
         /// </summary>
         private bool CanExecuteSelectTaskCommand(IcomiTask param)
         {
             return true;
         }
         /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
         protected virtual void ExecuteChangeSelectedCommand(string param)
        {
            if (param != null && param!=string.Empty)
            {
                if (this.SelectedIndex == 0)
                {
                    var inc = Int32.Parse(param);
                    if(inc>0)
                        this.SelectedIndex += inc; 
                }else if (this.SelectedIndex == this.LesNouveauxDocuments.Count-1)
                {
                    var inc = Int32.Parse(param);
                    if (inc < 0)
                        this.SelectedIndex += inc;
                }else
                    this.SelectedIndex += Int32.Parse(param); 
            }
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
         private bool CanExecuteChangeSelectedCommand(string param)
        {
            return true;
        }


        #endregion
    }
}
