using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_FW.Exception
{
    /// <summary>
    /// Classe de base des exceptions de l'application
    /// </summary>
    public abstract class ExceptionBase : System.Exception
    {

        #region Ctor

        /// <summary>
        /// Constructeur
        /// </summary>
        public ExceptionBase()
            : base()
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="msg">Un message</param>
        public ExceptionBase(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="msg">Un message</param>
        /// <param name="inner">L'exception initiale</param>
        public ExceptionBase(string msg, System.Exception inner)
            : base(msg, inner)
        {
        }

        #endregion

    }
}
