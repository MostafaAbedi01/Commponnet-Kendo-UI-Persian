using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;
using System.Drawing.Drawing2D;
using DImage = System.Drawing.Image;

namespace Mehr.Web
{
    public static class ImageHelper
    {
        public static SortedDictionary<string, ImageFormat> ValidFormats = new SortedDictionary<string, ImageFormat>()
        {
            {"jpe",ImageFormat.Jpeg},
            {"gif",ImageFormat.Gif},
            {"bmp",ImageFormat.Bmp},
            {"icon",ImageFormat.Icon},
            {"jpeg",ImageFormat.Jpeg},
            {"pjpeg",ImageFormat.Jpeg},
            {"x-png",ImageFormat.Png},
            {"vnd.microsoft.icon",ImageFormat.Icon},
            {"jpg",ImageFormat.Jpeg},
            {"png",ImageFormat.Png},
            {"tiff",ImageFormat.Tiff},
            {"wmf",ImageFormat.Wmf},
        };

        public static ImageFormat ConvertFromString(string name) { return ValidFormats[name.Trim().ToLower()]; }

        public static string ConvertToString(ImageFormat format)
        {
            if (format.Equals(ImageFormat.Gif)) return "gif";
            else if (format.Equals(ImageFormat.Bmp)) return "bmp";
            else if (format.Equals(ImageFormat.Icon)) return "icon";
            else if (format.Equals(ImageFormat.Jpeg)) return "jpeg";
            else if (format.Equals(ImageFormat.Png)) return "png";
            else if (format.Equals(ImageFormat.Tiff)) return "tiff";
            else if (format.Equals(ImageFormat.Wmf)) return "wmf";
            throw new NotSupportedException(string.Format("format '{0}'", format));
        }

        public static ImageFormat ConvertFromContentType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType) || !contentType.StartsWith("image/"))
                throw new NotSupportedException(string.Format("Content type '{0}'", contentType));

            string imageFormatString = contentType.Substring(6);
            return ConvertFromString(imageFormatString);
        }

        public static string ConvertToContentType(ImageFormat format)
        {
            string imageFormatString = ConvertToString(format);
            if (format.Equals(ImageFormat.Icon)) imageFormatString = "vnd.microsoft.icon";
            return string.Concat("image/", imageFormatString);
        }

        public static Image FromBytes(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length);
            return Image.FromStream(stream);
        }

        public static byte[] ToBytes(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static byte[] GetBytes(this HttpPostedFileBase file)
        {
            byte[] bytes = new byte[file.ContentLength];
            file.InputStream.Read(bytes, 0, file.ContentLength);
            return bytes;
        }

        public static ImageFormat GetImageFormat(this HttpPostedFileBase file)
        {
            return ConvertFromContentType(file.ContentType);
        }

        public static string Validate(this HttpPostedFileBase file, bool isRequired = true)
        {
            if (isRequired && file == null)
                return "فایل الزامی است.";
            else if (file != null)
            {
                if (
                    string.IsNullOrEmpty(file.ContentType) ||
                    !file.ContentType.StartsWith("image/") ||
                    !ValidFormats.ContainsKey(file.ContentType.Substring(6).Trim().ToLower()))
                    return "فرمت مجاز یکی از موارد (" + string.Join(",", ValidFormats.Keys) + ") است.";
                else if (file.ContentLength > 5 * 1024 * 1024)
                    return "حجم مجاز ۵ مگا بایت معادل ۵۱۲۰ کیلو بایت است.";
            }
            return null;
        }

        public static DImage GetThumbnail(this DImage image, int size = 40)
        {
            bool isWidthBigger = image.Width > image.Height;
            int maxSide = isWidthBigger ? image.Width : image.Height;
            float percent = (float)size / (float)maxSide;

            DImage thumbImage = image.GetThumbnailImage(
                isWidthBigger ? size : (int)(image.Width * percent),
                (!isWidthBigger) ? size : (int)(image.Height * percent),
                new System.Drawing.Image.GetThumbnailImageAbort(() => { return true; }), IntPtr.Zero);
            return thumbImage;
        }
    }
}
