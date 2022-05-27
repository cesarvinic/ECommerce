using System.IO;
using System.Web;

namespace ECommerce.Classes
{
    public class FilesHelper
    {
        /// <summary>
        /// Salva o diretório do arquivo no campo Foto e carrega a partir do diretório passado. 
        /// Mantém a integridade do nome do arquivo. 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            return pic;
        }

        /// <summary>
        /// Salva o diretório do arquivo no campo Foto e carrega a partir do diretório passado. 
        /// Altera o nome do arquivo para o Id da Empresa salva. 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string name)
        {
            string path = string.Empty;

            if (file == null ||
                string.IsNullOrWhiteSpace(folder) ||
                string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            else
            {
                try
                {
                    if (file != null)
                    {
                        path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                        file.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                    }
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                    //throw;
                }
            }
        }
    }
}