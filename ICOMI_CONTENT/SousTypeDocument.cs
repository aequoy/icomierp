using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_CONTENT
{
    public static class SousTypeDocumentFactory
    {
        public static IEnumerable<SousTypeDocument> GetListSousType()
        {
            yield return new SousTypeDocument() { Code = "PRV", Libelle = "Preuve" };
            yield return new SousTypeDocument() { Code = "ITN", Libelle = "Intention" };
        }
    }
    public class SousTypeDocument:RefObject
    {
    }
}
