using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_DOMAIN
{
    public class Recette
    {
        /// <summary>
        /// Obtient ou dÃ©finit le nom du document pour l'affichage
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Obtient ou dÃ©finit le titre du document pour l'affichage
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Obtient ou dÃ©finit la source du document pour l'affichage
        /// </summary>
        public string Source { get; set; }  
    }
}
