using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace ICOMI_FW.Exception
{
    public class BusinessException : ExceptionBase
    {
        #region Static

        /// <summary>
        /// Lève une <see cref="BusinessException"/> si l'objet spécifié est null
        /// </summary>
        /// <param name="o">Un objet</param>
        /// <param name="errorMessage">Un message d'erreur</param>
        public static void ThrowIfNull(object o, string errorMessage)
        {
            if (o == null)
            {
                MethodBase methodBase = (new StackTrace()).GetFrame(1).GetMethod();

                throw new BusinessException(methodBase.DeclaringType.Name, methodBase.Name, errorMessage);
            }
        }

        /// <summary>
        /// Lève une <see cref="BusinessException"/> si l'objet spécifié est non null
        /// </summary>
        /// <param name="o">Un objet</param>
        /// <param name="errorMessage">Un message d'erreur</param>
        public static void ThrowIfNotNull(object o, string errorMessage)
        {
            if (o != null)
            {
                MethodBase methodBase = (new StackTrace()).GetFrame(1).GetMethod();

                throw new BusinessException(methodBase.DeclaringType.Name, methodBase.Name, errorMessage);
            }
        }

        /// <summary>
        /// Lève une <see cref="BusinessException"/> si l'objet spécifié est null
        /// </summary>
        /// <param name="o">Un objet</param>
        /// <param name="errorCode">Un code d'erreur</param>
        /// <param name="errorMessage">Un message d'erreur</param>
        public static void ThrowIfNull(object o, string errorCode, string errorMessage)
        {
            if (o == null)
            {
                MethodBase methodBase = (new StackTrace()).GetFrame(1).GetMethod();

                throw new BusinessException(methodBase.DeclaringType.Name, methodBase.Name, errorCode, errorMessage);
            }
        }

        /// <summary>
        /// Lève une <see cref="BusinessException"/> si la chaine spécifiée est nulle ou vide
        /// </summary>
        /// <param name="s">Une chaine de caractère</param>
        /// <param name="errorMessage">Un message d'erreur</param>
        public static void ThrowIfNullOrEmpty(string s, string errorMessage)
        {
            if (string.IsNullOrEmpty(s))
            {
                MethodBase methodBase = (new StackTrace()).GetFrame(1).GetMethod();

                throw new BusinessException(methodBase.DeclaringType.Name, methodBase.Name, errorMessage);
            }
        }

        /// <summary>
        /// Lève une <see cref="BusinessException"/> si la chaine spécifiée est nulle ou vide
        /// </summary>
        /// <param name="s">Une chaine de caractère</param>
        /// <param name="errorCode">Un code d'erreur</param>
        /// <param name="errorMessage">Un message d'erreur</param>
        public static void ThrowIfNullOrEmpty(string s, string errorCode, string errorMessage)
        {
            if (string.IsNullOrEmpty(s))
            {
                MethodBase methodBase = (new StackTrace()).GetFrame(1).GetMethod();

                throw new BusinessException(methodBase.DeclaringType.Name, methodBase.Name, errorCode, errorMessage);
            }
        }

        #endregion

        

        #region Constructeurs

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">La classe à l'origine de l'exception</param>
        /// <param name="methode">La méthode à l'origine de l'exception</param>
        /// <param name="message">Le message d'erreur</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public BusinessException(string classe, string methode, string message)
            : base(message)
        {
            Source = string.Concat(classe, ".", methode);            
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">La classe à l'origine de l'exception</param>
        /// <param name="methode">La méthode à l'origine de l'exception</param>
        /// <param name="code">Le code de l'erreur</param>
        /// <param name="message">Le message d'erreur</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public BusinessException(string classe, string methode, string code, string message)
            : base(message)
        {
            Source = string.Concat(classe, ".", methode);
            Code = code;           
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">La classe à l'origine de l'exception</param>
        /// <param name="methode">La méthode à l'origine de l'exception</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public BusinessException(string classe, string methode)
            : base()
        {
            Source = string.Concat(classe, ".", methode);
            
        }


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">La classe à l'origine de l'exception</param>
        /// <param name="methode">La méthode à l'origine de l'exception</param>
        /// <param name="exception">L'exception d'origine</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public BusinessException(string classe, string methode, System.Exception exception)
            : base(exception.Message, exception)
        {
            Source = string.Concat(classe, ".", methode);           
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">La classe à l'origine de l'exception</param>
        /// <param name="methode">La méthode à l'origine de l'exception</param>
        /// <param name="message">Le message d'erreur</param>
        /// <param name="exception">L'exception d'origine</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public BusinessException(string classe, string methode, string message, System.Exception exception)
            : base(message, exception)
        {
            Source = string.Concat(classe, ".", methode);
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">La classe à l'origine de l'exception</param>
        /// <param name="methode">La méthode à l'origine de l'exception</param>
        /// <param name="code">Le code de l'erreur</param>
        /// <param name="message">Le message d'erreur</param>
        /// <param name="exception">L'exception d'origine</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public BusinessException(string classe, string methode, string code, string message, System.Exception exception)
            : base(message, exception)
        {
            Source = string.Concat(classe, ".", methode);
            Code = code;
        }

        #endregion

        #region Accesseurs

        /// <summary>
        /// Le code de l'erreur
        /// </summary>
        public string Code { get; protected set; }

        #endregion

    }
}
