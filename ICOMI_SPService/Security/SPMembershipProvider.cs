using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Microsoft.SharePoint.Client;

namespace ICOMI_SPService.Security
{
    public class SPMembershipProvider : MembershipProvider 
    {
        private string _ApplicationName = "ICOMI_SPService";
        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName=value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public ICOMI_DOMAIN.User ConnectUser(string username, string password)
        {
            try
            {
                var ctx = SharepointContextFactory.GetContext();
                var maListe = ctx.Web.Lists.GetByTitle("ICOMI_USERS");                
                string cquery = string.Format("<View><Query><Where><Eq><FieldRef Name='Username' /><Value Type='Text'>{0}</Value></Eq></Where></Query></View>", username);
                var camlQuery = new CamlQuery
                {
                    ViewXml = cquery
                };
                var resultat = maListe.GetItems(camlQuery);
                ctx.Load(resultat);
                

                ctx.ExecuteQuery();
                if (resultat.Count > 0)
                {
                    var listItem = resultat[0];
                    if (listItem.FieldValues["Password"] != null && listItem.FieldValues["Password"].ToString().Equals(password))
                    {
                        var user = new ICOMI_DOMAIN.User();
                        
                        user.UserName = listItem.FieldValues["Username"] != null ? listItem.FieldValues["Username"].ToString() : string.Empty;
                        int idImg = listItem.FieldValues["ImageUser"] != null ? (listItem.FieldValues["ImageUser"] as FieldLookupValue).LookupId : 0;
                        user.Nom = listItem.FieldValues["Title"] != null ? listItem.FieldValues["Title"].ToString() : string.Empty;
                        user.Prenom = listItem.FieldValues["FirstName"] != null ? listItem.FieldValues["FirstName"].ToString() : string.Empty;
                        user.NomComplet = listItem.FieldValues["FullName"] != null ? listItem.FieldValues["FullName"].ToString() : string.Empty;

                        DealWithUserImage(user, idImg);

                        return user;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur", ex);
            }
        }

        private static void DealWithUserImage(ICOMI_DOMAIN.User user, int idImg)
        {
            //Traitement de l'image
            string cquery2 = string.Format("<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Int'>{0}</Value></Eq></Where></Query></View>", idImg);
            var camlQuery2 = new CamlQuery
            {
                ViewXml = cquery2
            };

            //Ici on ouvre un contexte de données Web FullTrust via IE sinon aucune autorisation de downloader un document
            // Apparemment les claims fournies par WIF sont insuffisantes !! (TODO : TROUVER UNE ASTUCE)
            var ctx2 = SharepointContextFactory.GetFullTrustContext();
            var maListe2 = ctx2.Web.Lists.GetByTitle("IMAGES_USERS");
            var resultat2 = maListe2.GetItems(camlQuery2);
            ctx2.Load(resultat2);
            ctx2.ExecuteQuery();
            if (resultat2.Count > 0)
            {
                var listItem2 = resultat2[0];
                string filename = listItem2.FieldValues["FileRef"] as string;
                filename = filename.Split('/').Last();
                string savedFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename);
                if (!System.IO.File.Exists(savedFile))
                {
                    FileInformation fileInformation = Microsoft.SharePoint.Client.File.OpenBinaryDirect(ctx2, filename);
                    byte[] flux = fileInformation.Stream.GetBytes();
                    fileInformation.Stream.Close();
                    System.IO.File.WriteAllBytes(savedFile, flux);
                }
                user.ImgUrl = savedFile;
            }
        }
    }
}
