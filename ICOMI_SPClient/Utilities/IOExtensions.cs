using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICOMI_FW.Exception;

namespace ICOMI_SPClient.Utilities
{
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
            if (fileExtension == null || fileExtension.Equals(string.Empty))
            {
                throw new BusinessException(typeof(IOExtensions).FullName, "OpenAsFile", "L'extension du fichier doit être précisée");
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
