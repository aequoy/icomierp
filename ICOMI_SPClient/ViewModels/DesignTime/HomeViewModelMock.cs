using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ICOMI_DOMAIN;

namespace ICOMI_SPClient.ViewModels.DesignTime
{
    public class HomeViewModelMock
    {
         public ObservableCollection<DocumentMetadataDataContract> LesNouveauxDocuments { get; private set; }
         public ObservableCollection<IcomiTask> LesTachesToDo { get; private set; }
         public HomeViewModelMock()
         {
             LesNouveauxDocuments = new ObservableCollection<DocumentMetadataDataContract>();
             for (int i = 0; i < 20; i++)
             {
                 var dmDC = new DocumentMetadataDataContract();
                    dmDC.Auteur = "Alexandre Equoy";                    
                    dmDC.Commentaires="Je peux laisser un commentaire sur ce document";
                    dmDC.CreateDate = DateTime.Now;
                    dmDC.DocumentVersion=1;                    
                    dmDC.Producteur = "ACCESS IT";
                    dmDC.RubriqueDocument = "Lutte contre les nuisibles";
                    dmDC.SousTypeDocument = "Preuve";                    
                    dmDC.TypeDocument = "Contrat";
                    dmDC.RubriqueDocumentCode = "LN";
                    dmDC.SousTypeDocumentCode = "PRV";
                    dmDC.TypeDocumentCode = "CTN";
                    dmDC.Titre = "Catalogue de formation";
                    LesNouveauxDocuments.Add(dmDC);

             }
             LesTachesToDo = new ObservableCollection<IcomiTask>();
             for (int i = 0; i < 4; i++)
             {
                 LesTachesToDo.Add(new IcomiTask(string.Empty){Title="Prise de température dans les douches", TileTitle="Prise de T°",NbOccurence=1});
             }
         }
    }
}
