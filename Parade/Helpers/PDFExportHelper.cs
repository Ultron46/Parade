using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Parade.Helpers
{
    public class PDFExportHelper
    {
        public static MemoryStream CreatedPdf(DataTable dt)
        {
            Document document = new Document();

            //string path = Server.MapPath(".") + "//Files//sample.pdf";

            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, PDFData);
            document.Open();
            Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            //PdfPRow row = null;
            float[] widths = new float[dt.Columns.Count];
            for (int i = 0; i < widths.Length; i++)
            {
                widths[i] = 4f;
            }

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            //int iCol = 0;
            //string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Parade"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(r[i].ToString(), font5));
                    }
                }
            }
            document.Add(table);
            document.Close();
            return PDFData;
        }
    }
}