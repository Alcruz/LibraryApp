using System.IO;
using System.Web;

namespace LibraryApp.Classes
{
    public class FilesHelper
    {
        public static string UploadPhoto(HttpPostedFileBase file, string folder, string count)
        {
            string path = string.Empty;
            string picture = string.Empty;

            if (file != null)
            {
                picture = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), count + picture);
                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            return picture;
        }
    }
}