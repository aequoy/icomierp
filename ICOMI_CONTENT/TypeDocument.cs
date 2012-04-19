using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_CONTENT
{
    public static class TypeDocumentFactory
    {
        public static IEnumerable<TypeDocument> GetListType()
        {
            yield return new TypeDocument() { Code = "CTN", Libelle = "Contrat" };
            yield return new TypeDocument() { Code = "PLN", Libelle = "Plan" };
            yield return new TypeDocument() { Code = "FIC", Libelle = "Fiche descriptive" };
            yield return new TypeDocument() { Code = "PROC", Libelle = "Procédure" };
            yield return new TypeDocument() { Code = "AUT", Libelle = "Autre document" };
        }
    }
    public class TypeDocument : RefObject
    {
    }
}
