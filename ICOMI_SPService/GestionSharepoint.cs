using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using ICOMI_FW.Exception;
using System.Linq.Expressions;
using MSDN.Samples.ClaimsAuth;
using ICOMI_DOMAIN;
using System.IO;
using ICOMI_SPService.Security;

namespace ICOMI_SPService
{
    public class GestionSharepoint
    {

        /// <summary>
        /// Récupère la liste de users dans la librairie des users
        /// </summary>
        /// <param name="username">le login</param>
        /// <param name="password">le mot de passe</param>
        /// <param name="librairie">une librairie Sharepoint</param>
        /// <returns>L'utilisateur connecté ou null</returns>
        public ICOMI_DOMAIN.User ConnectUser(string username, string password)
        {
            SPMembershipProvider spS = new SPMembershipProvider();
            return spS.ConnectUser(username, password);
        }

        /// <summary>
        /// Récupère la liste de noms de fichiers dans une librairie Sharepoint
        /// </summary>
        /// <param name="librairie">une librairie Sharepoint</param>
        /// <returns>La liste de nom de fichiers dans une librairie donnée</returns>
        public List<string> GetFileList(string librairie)
        {
            try
            {
                var ctx = SharepointContextFactory.GetContext();
                
                    FileCollection files = ctx.Web.GetFolderByServerRelativeUrl(librairie).Files;
                    ctx.Load(files);
                    ctx.ExecuteQuery();
                    
                    List<string> docList = new List<string>();

                    foreach (Microsoft.SharePoint.Client.File file in files)
                        docList.Add(file.Name);

                    return docList;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de la liste des documents", ex);
            }
        }

		/// <summary>
		/// Ajoute un fichier dans Sharepoint
		/// </summary>
		/// <param name="libraryName">le nom de la librairie de stockage</param>
		/// <param name="metadatas">les metadatas reliées au document</param>
		/// <param name="content">le document sous forme de tableau de bytes</param>
		/// <param name="extension">l'extension du fichier ajouté</param>
		/// <returns>le nom du fichier tel qu'il est connu dans sharepoint</returns>
		public string AddFile(string libraryName, DocumentMetadataDataContract metadatas, byte[] content, string extension)
		{
			BusinessException.ThrowIfNullOrEmpty(libraryName, "La librairie sharepoint contenant les documents n'a pas été spécifiée dans le fichier de configuration");
			BusinessException.ThrowIfNullOrEmpty(extension, "L'extension n'a pas été renseignée.");
			BusinessException.ThrowIfNull(metadatas, "Les métadatas n'ont pas été renseignées.");
            BusinessException.ThrowIfNullOrEmpty(metadatas.RubriqueDocument, "La rubrique du document n'a pas été renseigné.");
            BusinessException.ThrowIfNullOrEmpty(metadatas.TypeDocument, "Le type du document n'a pas été renseigné.");
            BusinessException.ThrowIfNullOrEmpty(metadatas.SousTypeDocument, "Le sstype du document n'a pas été renseigné.");
            BusinessException.ThrowIfNullOrEmpty(metadatas.DocumentVersion.ToString(), "La Version du document n'a pas été renseigné.");

            try
            {
                extension = string.Concat(!extension.StartsWith(".", StringComparison.Ordinal) ? "." : "", extension);

                // Génération du nom du fichier (son identifiant) LN_PRV_CTN_100320121234321234_1
                string codeName = string.Concat(metadatas.RubriqueDocumentCode, "_", metadatas.TypeDocumentCode, "_", metadatas.SousTypeDocumentCode, "_", DateTime.Now.ToString("ddMMyyyyhhmmssffff"), "_", metadatas.DocumentVersion.ToString());
                metadatas.CodeDocument = codeName;
                string fileName = string.Concat(codeName, extension);

                // Récupère le contexte sharepoint
                var ctx = SharepointContextFactory.GetFullTrustContext();
                if (ctx != null)
                {
                    //ctx.Load(ctx.Web); // Query for Web
                    //ctx.ExecuteQuery(); // Execute
                    ctx.Load(ctx.Web.Folders);
                    ctx.ExecuteQuery();
                    Microsoft.SharePoint.Client.Folder dir = null;
                    foreach (var dirItem in ctx.Web.Folders)
                    {
                        if (dirItem.Name.Equals(libraryName))
                            dir = dirItem;
                    }
                    if (dir != null)
                    {
                        FileCollection documentsFiles = dir.Files;
                        ctx.Load(documentsFiles);
                        ctx.ExecuteQuery();
                        // Charge la collection des documents de la librairie
                        //FileCollection documentsFiles = ctx.Web.GetFolderByServerRelativeUrl(libraryName).Files;
                        //ctx.Load(documentsFiles);
                        //ctx.ExecuteQuery();

                        // information sur le fichier
                        FileCreationInformation fciNewFileFromComputer = new FileCreationInformation();
                        fciNewFileFromComputer.Content = content;
                        fciNewFileFromComputer.Url = fileName;
                        fciNewFileFromComputer.Overwrite = true;

                        // Ajoute le fichier à la collection
                        Microsoft.SharePoint.Client.File newFile = documentsFiles.Add(fciNewFileFromComputer);
                        ctx.Load(newFile);

                        // recupération des metadatas existantes sur le fichier
                        Microsoft.SharePoint.Client.ListItem item = newFile.ListItemAllFields;
                        ctx.Load(item);

                        // ajout des metadatas au nouveau fichier
                        FillMetadatas(item, metadatas, fileName);

                        item.Update();
                        ctx.Load(item);
                        ctx.ExecuteQuery();

                        return fileName;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                throw new BusinessException(GetType().FullName, "UploadToSharepoint", "Erreur lors de l'envoi du document vers Sharepoint : " + ex.Message, ex);
            }
		}


		/// <summary>
		/// Remplit les metadatas d'un document
		/// </summary>
		/// <param name="item">l'item sharepoint représentant le fichier que l'on va traiter</param>
		/// <param name="metadata">des métadatas</param>
		/// <param name="fileName">le nom du fichier</param>
		protected void FillMetadatas(Microsoft.SharePoint.Client.ListItem item, DocumentMetadataDataContract metadata, string fileName)
		{
			item["CodeDocument"] = metadata.CodeDocument;
            item["Auteur"] = metadata.Auteur;
            item["Commentaires"] = metadata.Commentaires;
            item["DocumentVersion"] = metadata.DocumentVersion.ToString();
            item["NomDocument"] = fileName;            
			item["Producteur"] = metadata.Producteur;
			item["Title"] = metadata.Titre;
            item["RubriqueDocument"] = metadata.RubriqueDocument;
            item["TypeDocument"] = metadata.TypeDocument;
            item["SousTypeDocument"] = metadata.SousTypeDocument;
            item["RubriqueDocumentCode"] = metadata.RubriqueDocumentCode;
            item["TypeDocumentCode"] = metadata.TypeDocumentCode;
            item["SousTypeDocumentCode"] = metadata.SousTypeDocumentCode;
		}

        /// <summary>
        /// Tries the get list item.
        /// </summary>
        /// <param name="clientContext">The Sharepoint Client Context.</param>
        /// <param name="listTitle">The list title.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="caml">The caml.</param>
        /// <param name="listItemCollection">The list item collection.</param>
        /// <returns></returns>
        private bool TryGetListItem(ClientContext clientContext, string listTitle, string caml, out ListItemCollection listItemCollection)
        {
            listItemCollection = null;
            try
            {
                var list = clientContext.Web.Lists.GetByTitle(listTitle);

                var camlQuery = new CamlQuery
                {
                    ViewXml = string.IsNullOrEmpty(caml)
                                  ? @"<View/>"
                                  : caml
                };

                listItemCollection = list.GetItems(camlQuery);

                clientContext.Load(listItemCollection);
                clientContext.ExecuteQuery();
                return listItemCollection != null;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Obtenir la liste des meta données des courrier 
        /// </summary>
        /// <param name="id">liste des identifiant dossier</param>
        /// <param name="libraryName">nom de la lib</param>
        /// <param name="extension">extension dosseir</param>
        /// <returns></returns>
        public List<DocumentMetadataDataContract> GetAllDocumentMetaByRubrique(string rubriqueCode, string libraryName, out string extension)
        {
            // tester si le parametre est nulle;
            BusinessException.ThrowIfNull(rubriqueCode, "la rubrique n'est pas renseignée");

            extension = string.Empty;
            List<DocumentMetadataDataContract> listMeta = new List<DocumentMetadataDataContract>();
            try
            {
                // Récupère le contexte sharepoint
                var ctx = SharepointContextFactory.GetContext();

                string camlQuery = string.Format("<View><Query><Where><Eq><FieldRef Name='RubriqueDocumentCode' /><Value Type='Text'>{0}</Value></Eq></Where></Query></View>", rubriqueCode);

                ListItemCollection listItems;
                if (TryGetListItem(ctx, libraryName, camlQuery, out listItems))
                {
                    if (listItems.Count > 0)
                    {
                        foreach (ListItem listItem in listItems.ToList())
                        {
                            var dmDC = new DocumentMetadataDataContract();
                            dmDC.NomDocument = listItem.FieldValues["NomDocument"] != null ? listItem.FieldValues["NomDocument"].ToString() : string.Empty;
                            dmDC.CreateDate = listItem.FieldValues["Modified"] != null ? (DateTime)listItem.FieldValues["Modified"] : DateTime.Now;
                            dmDC.CodeDocument = listItem.FieldValues["CodeDocument"] != null ? listItem.FieldValues["CodeDocument"].ToString() : string.Empty;
                            dmDC.Commentaires = listItem.FieldValues["Commentaires"] != null ? listItem.FieldValues["Commentaires"].ToString() : string.Empty;
                            dmDC.RubriqueDocument = listItem.FieldValues["RubriqueDocument"] != null ? listItem.FieldValues["RubriqueDocument"].ToString() : string.Empty;
                            dmDC.TypeDocument = listItem.FieldValues["TypeDocument"] != null ? listItem.FieldValues["TypeDocument"].ToString() : string.Empty;
                            dmDC.SousTypeDocument = listItem.FieldValues["SousTypeDocument"] != null ? listItem.FieldValues["SousTypeDocument"].ToString() : string.Empty;
                            dmDC.RubriqueDocumentCode = listItem.FieldValues["RubriqueDocumentCode"] != null ? listItem.FieldValues["RubriqueDocumentCode"].ToString() : string.Empty;
                            dmDC.TypeDocumentCode = listItem.FieldValues["TypeDocumentCode"] != null ? listItem.FieldValues["TypeDocumentCode"].ToString() : string.Empty;
                            dmDC.SousTypeDocumentCode = listItem.FieldValues["SousTypeDocumentCode"] != null ? listItem.FieldValues["SousTypeDocumentCode"].ToString() : string.Empty;
                            dmDC.DocumentVersion = listItem.FieldValues["DocumentVersion"] != null ? Int32.Parse(listItem.FieldValues["DocumentVersion"].ToString()) : 1;
                            dmDC.Producteur = listItem.FieldValues["Producteur"] != null ? listItem.FieldValues["Producteur"].ToString() : string.Empty;
                            dmDC.Titre = listItem.FieldValues["Title"] != null ? listItem.FieldValues["Title"].ToString() : string.Empty;
                            dmDC.Auteur = listItem.FieldValues["Auteur"] != null ? listItem.FieldValues["Auteur"].ToString() : string.Empty;
                            listMeta.Add(dmDC);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    throw new BusinessException(GetType().FullName, "GetAllDocumentMetaByRubrique", string.Format("Erreur lors de la récupération des documents de la rubrique '{0}' dans Sharepoint.", rubriqueCode));
                }

                return listMeta;
            }
            catch (ExceptionBase)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException(GetType().FullName, "GetAllDocumentMetaByRubrique", string.Format("Erreur lors de la récupération des documents de la rubrique '{0}' dans Sharepoint : {1}", rubriqueCode, ex.Message), ex);
            }
        }

        /// <summary>
		/// Récupère le document spécifié dans la librairie spécifiée
		/// </summary>
		/// <param name="idDocument">l'id du document à récupérer</param>
		/// <param name="libraryName">le nom de la librairie</param>
		/// <param name="extension">fournit l'extension du fichier récupéré</param>
		/// <returns>le fichier désiré sous forme de tableau de bytes</returns>
        public byte[] GetFile(string NomDocument, string libraryName, out string extension)
        {

            extension = string.Empty;
            try
            {
                ClientContext ctx = SharepointContextFactory.GetFullTrustContext();
                string camlQuery = string.Format("<View><Query><Where><Eq><FieldRef Name='NomDocument' /><Value Type='Text'>{0}</Value></Eq></Where></Query></View>", NomDocument);
                    ListItemCollection listItems;
                    if (TryGetListItem(ctx, libraryName, camlQuery, out listItems))
                    {

                        if (listItems.Count > 0)
                        {
                            string fileRef = listItems.AsEnumerable().Select(doc => doc.FieldValues["FileRef"] as string).FirstOrDefault();
                            
                            FileInformation fileInformation = Microsoft.SharePoint.Client.File.OpenBinaryDirect(ctx, fileRef);
                            byte[] flux = fileInformation.Stream.GetBytes();
                            fileInformation.Stream.Close();
                            extension = fileRef.Substring(fileRef.LastIndexOf('.'));
                            return flux;
                        }
                        else
                            throw new BusinessException(GetType().FullName, "GetFile", string.Format("Document '{0}' non trouvé dans Sharepoint.",NomDocument));
                    }
                    else
                        throw new BusinessException(GetType().FullName, "GetFile", string.Format("Erreur lors de la récupération du document '{0}' dans Sharepoint.",NomDocument));
                
            }
            catch (ExceptionBase ex)
            {                
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException(GetType().FullName, "GetFile", string.Format("Erreur lors de la récupération du document '{0}' dans Sharepoint : {1}",NomDocument, ex.Message), ex);
            }
        }


        /// <summary>
        /// Récupère les documents qui ont changé dans les librairies
        /// </summary>
        /// <returns>la liste des documents</returns>
        public List<DocumentMetadataDataContract> GetAllChangedDocumentMetaByRubrique(string libraryName)
        {
            List<DocumentMetadataDataContract> listMeta = new List<DocumentMetadataDataContract>();
            try
            {
                // Récupère le contexte sharepoint
                var ctx = SharepointContextFactory.GetContext();

                string camlQuery = "<Where><Geq><FieldRef Name='Modified'/><Value Type='DateTime'><Today OffsetDays='-20'/></Value></Geq> </Where>";

                ListItemCollection listItems;
                if (TryGetListItem(ctx, libraryName, camlQuery, out listItems))
                {
                    if (listItems.Count > 0)
                    {
                        foreach (ListItem listItem in listItems.ToList())
                        {
                            var dmDC = new DocumentMetadataDataContract();
                            dmDC.NomDocument = listItem.FieldValues["NomDocument"] != null ? listItem.FieldValues["NomDocument"].ToString() : string.Empty;
                            dmDC.CreateDate = listItem.FieldValues["Modified"] != null ? (DateTime)listItem.FieldValues["Modified"] : DateTime.Now;
                            dmDC.CodeDocument = listItem.FieldValues["CodeDocument"] != null ? listItem.FieldValues["CodeDocument"].ToString() : string.Empty;
                            dmDC.Commentaires = listItem.FieldValues["Commentaires"] != null ? listItem.FieldValues["Commentaires"].ToString() : string.Empty;
                            dmDC.RubriqueDocument = listItem.FieldValues["RubriqueDocument"] != null ? listItem.FieldValues["RubriqueDocument"].ToString() : string.Empty;
                            dmDC.TypeDocument = listItem.FieldValues["TypeDocument"] != null ? listItem.FieldValues["TypeDocument"].ToString() : string.Empty;
                            dmDC.SousTypeDocument = listItem.FieldValues["SousTypeDocument"] != null ? listItem.FieldValues["SousTypeDocument"].ToString() : string.Empty;
                            dmDC.RubriqueDocumentCode = listItem.FieldValues["RubriqueDocumentCode"] != null ? listItem.FieldValues["RubriqueDocumentCode"].ToString() : string.Empty;
                            dmDC.TypeDocumentCode = listItem.FieldValues["TypeDocumentCode"] != null ? listItem.FieldValues["TypeDocumentCode"].ToString() : string.Empty;
                            dmDC.SousTypeDocumentCode = listItem.FieldValues["SousTypeDocumentCode"] != null ? listItem.FieldValues["SousTypeDocumentCode"].ToString() : string.Empty;
                            dmDC.DocumentVersion = listItem.FieldValues["DocumentVersion"] != null ? Int32.Parse(listItem.FieldValues["DocumentVersion"].ToString()) : 1;
                            dmDC.Producteur = listItem.FieldValues["Producteur"] != null ? listItem.FieldValues["Producteur"].ToString() : string.Empty;
                            dmDC.Titre = listItem.FieldValues["Title"] != null ? listItem.FieldValues["Title"].ToString() : string.Empty;
                            dmDC.Auteur = listItem.FieldValues["Auteur"] != null ? listItem.FieldValues["Auteur"].ToString() : string.Empty;
                            listMeta.Add(dmDC);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    throw new BusinessException(GetType().FullName, "GetAllChangedDocumentMetaByRubrique", string.Format("Erreur lors de la récupération des documents qui ont changé dans Sharepoint."));
                }

                return listMeta;
            }
            catch (ExceptionBase)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException(GetType().FullName, "GetAllChangedDocumentMetaByRubrique", string.Format("Erreur lors de la récupération des documents qui ont changé dans Sharepoint : {1}", ex.Message), ex);
            }
        }



        /// <summary>
        /// Récupère les tâches du jour pour un utilisateur
        /// </summary>
        /// <param name="NomComplet">Nom complet de l'utilisateur</param>
        /// <returns>la liste des documents</returns>
        public IEnumerable<IcomiTask> GetTodaysTasksForUser(string NomComplet)
        {
            try
            {
                var ctx = SharepointContextFactory.GetContext();

                var list = ctx.Web.Lists.GetByTitle("ICOMI_ERP_PLANIF");
                //string query = String.Format(@"<View><Query> <Where><And><Eq> <FieldRef Name='User'/> <Value Type='Lookup'>{0}</Value> </Eq> <Geq><FieldRef Name='EndDate'/><Value Type='DateTime'><Today OffsetDays='0'/></Value></Geq></And></Where> </Query></View>", NomComplet);
                //<DateRangesOverlap> <FieldRef Name='EventDate' /> <FieldRef Name='EndDate' /> <FieldRef Name='RecurrenceID'/> <Value Type='DateTime'> <Today /> </Value> </DateRangesOverlap> 
                string query = String.Format(@"<View scope='RecursiveAll'><Query> <Where><And><Eq> <FieldRef Name='User'/> <Value Type='Lookup'>{0}</Value> </Eq> <DateRangesOverlap> <FieldRef Name='EventDate' /> <FieldRef Name='EndDate' /> <FieldRef Name='RecurrenceID'/> <Value Type='DateTime'> <Today /> </Value> </DateRangesOverlap> </And></Where> </Query></View>", NomComplet);
                var camlQuery = new CamlQuery
                {
                    ViewXml = query,

                };
                
                var tasks = list.GetItems(camlQuery);
                ctx.Load(tasks);
                ctx.ExecuteQuery();
                IcomiTask maTache = null;
                List<IcomiTask> lesTaches = new List<IcomiTask>();
                foreach (var listItem in tasks)
                {
                    maTache = new IcomiTask(NomComplet);
                    maTache.NbOccurence = 1;
                    maTache.ID = listItem.FieldValues["DateDebut"] != null ? listItem.FieldValues["DateDebut"].ToString() : "00:00:00";
                    maTache.HeureDebut = listItem.FieldValues["DateDebut"] != null ? listItem.FieldValues["DateDebut"].ToString() : "00:00:00";
                    maTache.HeureFin = listItem.FieldValues["DateFin"] != null ? listItem.FieldValues["DateFin"].ToString() : "00:00:00";

                    bool allDayEvent = listItem.FieldValues["fAllDayEvent"] != null ? Convert.ToBoolean(listItem.FieldValues["fAllDayEvent"]) : false;
                    if (allDayEvent)
                    {
                        maTache.HeureDebut = "Toute la journée";
                        maTache.HeureFin = string.Empty;
                    }
                    maTache.TileTitle = listItem.FieldValues["Title"] != null ? listItem.FieldValues["Title"].ToString() : string.Empty;
                    maTache.Title = listItem.FieldValues["Title"] != null ? listItem.FieldValues["Title"].ToString() : string.Empty;
                    maTache.Emplacement = listItem.FieldValues["Location"] != null ? listItem.FieldValues["Location"].ToString() : string.Empty;
                    maTache.Description = listItem.FieldValues["Description"] != null ? listItem.FieldValues["Description"].ToString() : string.Empty;
                    if(!maTache.Title.Contains("Suppression"))
                        lesTaches.Add(maTache); 
                }
                return lesTaches;
            }
            catch (ExceptionBase)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException(GetType().FullName, "GetTodaysTasksForUser", string.Format("Erreur lors de la récupération des tâches de l'utilisateur : {0}", ex.Message), ex);
            }
        }


        /// <summary>
        /// Récupère les recettes
        /// </summary>
        /// <returns>la liste des recettes</returns>
        public IEnumerable<Recette> GetListRecette()
        {
            try
            {
                var ctx = SharepointContextFactory.GetFullTrustContext();

                var list = ctx.Web.Lists.GetByTitle("ICOMI_RECETTES");

                string query = @"<View/>)";
                var camlQuery = new CamlQuery
                {
                    ViewXml = query,

                };

                var tasks = list.GetItems(camlQuery);
                ctx.Load(tasks);
                ctx.ExecuteQuery();
                Recette maRecette = null;
                List<Recette> lesRecettes = new List<Recette>();
                foreach (var listItem in tasks)
                {
                    maRecette = new Recette();
                    maRecette.ID = listItem.FieldValues["FileRef"] != null ? listItem.FieldValues["FileRef"].ToString() : string.Empty;
                    maRecette.Title = listItem.FieldValues["Title"] != null ? listItem.FieldValues["Title"].ToString() :string.Empty;
                    string nameFile = maRecette.ID.Substring(maRecette.ID.LastIndexOf('/') + 1);
                    string savedFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), nameFile);
                    maRecette.Source = savedFile; 
                    if(!System.IO.File.Exists(savedFile)){
                        FileInformation fileInformation = Microsoft.SharePoint.Client.File.OpenBinaryDirect(ctx, maRecette.ID);
                        byte[] flux = fileInformation.Stream.GetBytes();
                        fileInformation.Stream.Close();                    
                        System.IO.File.WriteAllBytes(savedFile, flux);  
                    }
                    lesRecettes.Add(maRecette);
                   
                }
                return lesRecettes;
            }
            catch (ExceptionBase)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException(GetType().FullName, "GetListRecette", string.Format("Erreur lors de la récupération des recettes : {0}", ex.Message), ex);
            }
        }
    }

    public static class IOExtensions
    {
        /// <summary>
        /// Retourne les octets du flux spécifié
        /// </summary>
        /// <param name="stream">Un flux</param>
        /// <returns></returns>
        public static byte[] GetBytes(this Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            byte[] buffer = new byte[32768];

            using (MemoryStream ms = new MemoryStream())
            {
                int read = 0;

                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Retourne un flux constitué à partir du tableau d'octets spécifié
        /// </summary>
        /// <param name="bytes">Un tableau d'octets</param>
        /// <returns></returns>
        public static Stream GetStream(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Crée un fichier temporaire avec le tableau d'octet comme contenu et lance ensuite l'ouverture de ce fichier avec le logiciel approprié
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="fileExtension"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122")]
        public static void OpenAsFile(this byte[] bytes, string fileExtension)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (!fileExtension.StartsWith("."))
            {
                fileExtension = string.Concat(".", fileExtension);
            }

            string savedFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName() + fileExtension);

            try
            {
                using (FileStream fs = new FileStream(savedFile, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }

                System.Diagnostics.Process.Start(savedFile);
            }
            catch (Exception ex)
            {
                throw new BusinessException(typeof(IOExtensions).FullName, "Open", "Erreur lors de l'ouverture du fichier", ex);
            }
        }
    }
}
