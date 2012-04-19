using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_DOMAIN
{
    public class DocumentMetadataDataContract
    {
        #region Properties (15)
        /// <summary>
        /// Obtient ou dÃ©finit l'auteur du document (c'est le qui de la dÃ©marche qualitÃ©)
        /// </summary>       
        public string Auteur { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit la code du document (la codification est une partie du nom)
        /// </summary>
        public string CodeDocument { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit les eventuels commentaires place sur le document
        /// </summary>
        public string Commentaires { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le numero de version (la version est un entier)
        /// </summary>
        public int DocumentVersion { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit la date et l'heure Ã  laquelle le document a Ã©tÃ© crÃ©Ã©
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le nom du document pour l'affichage
        /// </summary>
        public string NomDocument{ get; set; }       
        /// <summary>
        /// Obtient ou dÃ©finit le titre du document
        /// </summary>
        public string Titre { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le producteur du document (l'auteur, une société externe, un contrôleur, un consultant...)
        /// </summary>
        public string Producteur { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit la Rubrique du document (Luttes contre les nuisibles, qualité de l'eau, ...)
        /// </summary>
        public string RubriqueDocument { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le Type du document (intention, preuve)
        /// </summary>
        public string TypeDocument { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le SousType du document (contrat, photo, formulaire....)
        /// </summary>
        public string SousTypeDocument { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit la Rubrique du document (Luttes contre les nuisibles, qualité de l'eau, ...)
        /// </summary>
        public string RubriqueDocumentCode { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le Type du document (intention, preuve)
        /// </summary>
        public string TypeDocumentCode { get; set; }
        /// <summary>
        /// Obtient ou dÃ©finit le SousType du document (contrat, photo, formulaire....)
        /// </summary>
        public string SousTypeDocumentCode { get; set; }

        #endregion Properties
    }
}
