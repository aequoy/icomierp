using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using MSDN.Samples.ClaimsAuth;
using ClientOmAuth;

namespace ICOMI_SPService
{
    public class SharepointContextFactory
    {
        private static string _sharePointUrl = Settings.Default.SharePointServer;
        private static ClientContext ctx = null;
        /// <summary>
        /// Obtient l'URL du serveur Sharepoint
        /// </summary>
        public static string SharepointUrl
        {
            get
            {
                if (_sharePointUrl == null)
                {
                    throw new Exception("L'URL du serveur Sharepoint n'a pas été spécifié dans le fichier de configuration");
                }
                return _sharePointUrl;
            }
            private set { _sharePointUrl = value; }
        }

        public static ClientContext GetContext()
        {
            if (ctx == null)
            {
                //ctx = ClaimClientContext.GetAuthenticatedContext(SharepointUrl);
                MsOnlineClaimsHelper claimsHelper = new MsOnlineClaimsHelper(SharepointUrl, "aequoy@icomi.onmicrosoft.com", "1418Taktus");
                ctx = new ClientContext(SharepointUrl);
                ctx.ExecutingWebRequest += claimsHelper.clientContext_ExecutingWebRequest;
            }
            return ctx;
        }


        public static ClientContext GetFullTrustContext()
        {
            return ClaimClientContext.GetAuthenticatedContext(SharepointUrl);
        }
    }
}
