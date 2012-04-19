using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_CONTENT
{
    public static class RubriqueFactory
    {
        public static IEnumerable<Rubrique> GetListRubriques()
        {
            yield return new Rubrique() { Code = "LN", Libelle = "Lutte contre les nuisibles" };
            yield return new Rubrique() { Code = "QE", Libelle = "Qualité de l'eau" };
            yield return new Rubrique() { Code = "FP", Libelle = "Formation du personnel" };
            yield return new Rubrique() { Code = "SN", Libelle = "Santé" };
            yield return new Rubrique() { Code = "TN", Libelle = "Tenue" };
        }
    }


    public class Rubrique : RefObject
    {        
        
    }
}
