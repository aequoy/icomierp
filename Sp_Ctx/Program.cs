using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using System.Net;
using MSDN.Samples.ClaimsAuth;
using ICOMI_SPService;
using System.Configuration;
using ICOMI_DOMAIN;

namespace Sp_Ctx
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //if (args.Length < 1) { Console.WriteLine("SP_Ctx <url>"); return; }

            //string targetSite = args[0];
            //using (ClientContext ctx = ClaimClientContext.GetAuthenticatedContext(targetSite))
            //{
            //    if (ctx != null)
            //    {
            //        ctx.Load(ctx.Web); // Query for Web
            //        ctx.ExecuteQuery(); // Execute
            //        var list = ctx.Web.Lists.GetByTitle("ICOMI_ERP_PLANIF");
            //        var camlQuery = new CamlQuery
            //        {
            //            ViewXml = @"<View/>"
            //        };
            //        var listItemCollection = list.GetItems(camlQuery);
            //        ctx.Load(listItemCollection);
            //        ctx.ExecuteQuery();
            //        string s = string.Empty;
            //        foreach (var item in listItemCollection)
            //        {
            //            foreach (var key in item.FieldValues.Keys)
            //            {
            //                Console.WriteLine(key);
            //                Console.WriteLine(item.FieldValues[key]);
            //            }                           
            //        }					
            //    }
            //}
            //using (ClientContext ctx = ClaimClientContext.GetAuthenticatedContext(GestionSharepoint.SharepointUrl))
            //{
            //    if (ctx != null)
            //    {
                    //ctx.Load(ctx.Web); // Query for Web
                    //ctx.ExecuteQuery(); // Execute
                   
                    //foreach (var item in ctx.Web.Folders)
                    //{
                    //    ctx.Load(item.Files);
                    //    ctx.ExecuteQuery();
                    //}


                    var gSP = new GestionSharepoint();

                    //Sharepoint.QueryServiceSoapClient monClient = new Sharepoint.QueryServiceSoapClient();
                    //monClient.ClientCredentials.UserName.UserName = "aequoy@icomi.onmicrosoft.com";
                    //monClient.ClientCredentials.UserName.Password = "1418Taktus";

                    //System.Data.DataSet queryResults = monClient.QueryEx(GetXMLString());


                    var ctx = SharepointContextFactory.GetContext();
                    
                    var list = ctx.Web.Lists.GetByTitle("ICOMI_ERP_PLANIF");
                    ctx.Load(list);                        
                    ctx.ExecuteQuery();

                    string dede = @"<View><Query> <Where> <DateRangesOverlap> <FieldRefName='EventDate' /> <FieldRefName='EndDate' /> <FieldRefName='RecurrenceID' /> <ValueType='DateTime'> <Today /> </Value> </DateRangesOverlap> </Where> </Query> <QueryOptions> <ExpandRecurrence>TRUE</ExpandRecurrence> <CalendarDate> <Today /> </CalendarDate> <ViewAttributesScope='RecursiveAll' /> </QueryOptions> </View>";
                    string dede2 = @"<View><Query>  <Where><DateRangesOverlap> <FieldRef Name='EventDate' /> <FieldRef Name='EndDate' /> <FieldRef Name='RecurrenceID'/> <Value Type='DateTime'> <Today /> </Value> </DateRangesOverlap>  </Where> </Query></View>";
                    string dede3 = "<View scope='RecursiveAll'><Query> <Where><And><Eq> <FieldRef Name='User'/> <Value Type='Lookup'>Alexandre Equoy</Value> </Eq> <DateRangesOverlap> <FieldRef Name='EventDate' /> <FieldRef Name='EndDate' /> <FieldRef Name='RecurrenceID'/> <Value Type='DateTime'> <Today /> </Value> </DateRangesOverlap> </And></Where> </Query></View>";
                
                    var camlQuery = new CamlQuery
                            {
                                ViewXml = dede3
                            };
                    // </And>LookupId='TRUE' 
                    var l = list.GetItems(camlQuery);

                    ctx.Load(l);
                    ctx.ExecuteQuery();
                    foreach (var item in l)
                    {
                        string s = item.FieldValues["StartTime"].ToString();
                        foreach (var item2 in item.FieldValues)
                        {
                            
                        }
                        
                    }
                    ChangeToken ct = null;
                    var q = list.GetChanges(new ChangeQuery() { Add = true });
                   //ctx.Web.GetChanges( Add = true, Update = true, File = true });
                    ctx.Load(q);
                    ctx.ExecuteQuery();
                    while (q.Count > 0)
                    {
                    }

                    string tempDir = @"C:\Users\aequoy.ACCESSIT\Documents";
                    string fileTest = @"C:\Users\aequoy.ACCESSIT\Documents\catalogue_formation_2012_normal.pdf";
                    string LibraryName = "ICOMI_BPH";
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

                   //gSP.AddFile(LibraryName, dmDC, System.IO.File.ReadAllBytes(fileTest), ".PDF");
                    string extension = string.Empty;
                    var ll = gSP.GetAllDocumentMetaByRubrique("LN", LibraryName, out extension);
            //    }
            //}

            Console.ReadLine();
        }

        private static string GetXMLString()
            {
            /* 
               The proceeding six lines of code is actually one line of code.
               It is separated into four lines here for readability.
               You will need to remove the line breaks when you copy the code to your project.
            */
               StringBuilder xmlString = new StringBuilder("<QueryPacket xmlns='urn:Microsoft.Search.Query'><Query><SupportedFormats><Format revision='1'> urn:Microsoft.Search.Response.Document:Document</Format></SupportedFormats><Context><QueryText language='fr-FR' type='STRING'>");
                xmlString.Append("calendrier");
                xmlString.Append("</QueryText></Context></Query></QueryPacket>");
                return xmlString.ToString();
            }

    }
}
