using System.Drawing.Imaging;
using PdfiumViewer;
using System;
using System.Drawing;
using System.IO;
using System.Text;



namespace EntityFrameworkSP_Demo.Entities
{
    public class PdfToImageConverter
    {
        public static string ConvertPdfToBase64(string pdfFilePath)
        {
            using (var pdfDocument = PdfiumViewer.PdfDocument.Load(pdfFilePath))
            {
                var imageBytesList = new List<byte[]>();

                for (int i = 0; i < pdfDocument.PageCount; i++)
                {
                    using (var bitmap = pdfDocument.Render(i, 300, 300, true))
                    {
                        using (var ms = new MemoryStream())
                        {
                            bitmap.Save(ms, ImageFormat.Png);
                            imageBytesList.Add(ms.ToArray());
                        }
                    }
                }


                // Concatenate all image bytes into a single byte array
                var combinedBytes = imageBytesList.SelectMany(b => b).ToArray();

                // Convert byte array to base64 string
                return Convert.ToBase64String(combinedBytes);
            }
        }
    }
}
