using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class PDFMaker
    {
        public static byte[] CreatePDF(Mortgage mortgage)
        {
            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, output);
                doc.Open();

                Paragraph header = new Paragraph("Your Mortgage PDF");
                Paragraph paragraph = new Paragraph($"Amount {mortgage.Amount}.");

                doc.Add(header);
                doc.Add(paragraph);

                doc.Close();
                return output.ToArray();
            }
        }
    }
}
