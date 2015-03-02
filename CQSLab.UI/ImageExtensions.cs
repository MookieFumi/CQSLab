using System.Drawing;
using System.IO;
using System.Web;

namespace CQSLab.UI
{
    public static class ImageExtensions
    {
        public static byte[] GetImageBinaryFromHttpPostedFile(HttpPostedFileBase imagen)
        {
            var binary = new byte[imagen.ContentLength];
            imagen.InputStream.Read(binary, 0, imagen.ContentLength);

            return binary;
        }

        public static byte[] ImageToByteArray(this Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}