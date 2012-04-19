using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICOMI_CONTENT;
using Microsoft.SharePoint.Client;

namespace ICOMI_SPService
{
    public static class RefObjectSPService
    {
        private static IEnumerable<Rubrique> LesRubriquesChargees = null;
        private static IEnumerable<TypeDocument> LesTypesDocumentCharges = null;
        private static IEnumerable<SousTypeDocument> LesSousTypesDocumentCharges = null;

        public static IEnumerable<Rubrique> GetRubriques()
        {
            try
            {
                if (LesRubriquesChargees == null)
                {
                    var ctx = SharepointContextFactory.GetContext();
                    var maListe = ctx.Web.Lists.GetByTitle("ICOMI_RUBRIQUES");
                    string cquery = @"<View/>";
                    var camlQuery = new CamlQuery()
                    {
                        ViewXml = cquery
                    };
                    var resultat = maListe.GetItems(camlQuery);
                    ctx.Load(resultat);
                    ctx.ExecuteQuery();
                    List<Rubrique> lesRubriques = new List<Rubrique>();
                    Rubrique r = null;
                    if (resultat.Count > 0)
                    {
                        foreach (var item in resultat)
                        {
                            r = new Rubrique();
                            r.Code = item.FieldValues["Code"] != null ? item.FieldValues["Code"].ToString() : string.Empty;
                            r.Libelle = item.FieldValues["Libelle"] != null ? item.FieldValues["Libelle"].ToString() : string.Empty;
                            lesRubriques.Add(r);
                        }//fin foreach
                    }
                    LesRubriquesChargees = lesRubriques;
                }
                return LesRubriquesChargees;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur", ex);
            }
        }


        public static IEnumerable<TypeDocument> GetTypeDocument()
        {
            try
            {
                if (LesTypesDocumentCharges == null)
                {
                    var ctx = SharepointContextFactory.GetContext();
                    var maListe = ctx.Web.Lists.GetByTitle("TYPEDOCUMENT");
                    string cquery = @"<View/>";
                    var camlQuery = new CamlQuery()
                    {
                        ViewXml = cquery
                    };
                    var resultat = maListe.GetItems(camlQuery);
                    ctx.Load(resultat);
                    ctx.ExecuteQuery();
                    List<TypeDocument> lesTypeDocuments = new List<TypeDocument>();
                    TypeDocument t = null;
                    if (resultat.Count > 0)
                    {
                        foreach (var item in resultat)
                        {
                            t = new TypeDocument();
                            t.Code = item.FieldValues["Code"] != null ? item.FieldValues["Code"].ToString() : string.Empty;
                            t.Libelle = item.FieldValues["Libelle"] != null ? item.FieldValues["Libelle"].ToString() : string.Empty;
                            lesTypeDocuments.Add(t);
                        }//fin foreach
                    }
                    LesTypesDocumentCharges = lesTypeDocuments;
                }
                return LesTypesDocumentCharges;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur", ex);
            }
        }

        public static IEnumerable<SousTypeDocument> GetSousTypeDocument()
        {
            try
            {
                if (LesSousTypesDocumentCharges == null)
                {
                    var ctx = SharepointContextFactory.GetContext();
                    var maListe = ctx.Web.Lists.GetByTitle("SOUS_TYPE_DOCUMENT");
                    string cquery = @"<View/>";
                    var camlQuery = new CamlQuery()
                    {
                        ViewXml = cquery
                    };
                    var resultat = maListe.GetItems(camlQuery);
                    ctx.Load(resultat);
                    ctx.ExecuteQuery();
                    List<SousTypeDocument> lesSousTypeDocuments = new List<SousTypeDocument>();
                    SousTypeDocument st = null;
                    if (resultat.Count > 0)
                    {
                        foreach (var item in resultat)
                        {
                            st = new SousTypeDocument();
                            st.Code = item.FieldValues["Code"] != null ? item.FieldValues["Code"].ToString() : string.Empty;
                            st.Libelle = item.FieldValues["Libelle"] != null ? item.FieldValues["Libelle"].ToString() : string.Empty;
                            lesSousTypeDocuments.Add(st);
                        }//fin foreach
                    }
                    LesSousTypesDocumentCharges = lesSousTypeDocuments;
                }
                return LesSousTypesDocumentCharges;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur", ex);
            }
        }

    }
}
