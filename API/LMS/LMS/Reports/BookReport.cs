using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LMS.Entities;

namespace Library_Management.Reports
{
    public class BookReport
    {
        public static void ExportBooksToPDF(
            List<Book> books,
            string pdfPath,
            string reportTitle,
            DateTime startDate,
            DateTime endDate,
            float[] columnWidths = null,
            string logoPath = null)
        {
            if (books == null || books.Count == 0)
            {
                throw new Exception("No data to export.");
            }

            try
            {
                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                doc.Open();

                // HEADER TABLE
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 4f });

                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(50f, 50f);
                    PdfPCell logoCell = new PdfPCell(logo)
                    {
                        Border = Rectangle.NO_BORDER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    headerTable.AddCell(logoCell);
                }
                else
                {
                    headerTable.AddCell(new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER });
                }

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                headerTable.AddCell(new PdfPCell(new Phrase(reportTitle, titleFont))
                {
                    Border = Rectangle.NO_BORDER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                doc.Add(headerTable);
                doc.Add(new Paragraph("\n"));

                // DATE TABLE
                PdfPTable dateTable = new PdfPTable(2);
                dateTable.WidthPercentage = 50;
                dateTable.SetWidths(new float[] { 1f, 1f });

                dateTable.AddCell(new PdfPCell(new Phrase("Start Date: " + startDate.ToShortDateString())) { Border = Rectangle.NO_BORDER });
                dateTable.AddCell(new PdfPCell(new Phrase("End Date: " + endDate.ToShortDateString())) { Border = Rectangle.NO_BORDER });

                doc.Add(dateTable);
                doc.Add(new Paragraph("\n"));

                // BOOKS TABLE
                PdfPTable table = new PdfPTable(4); // adjust columns based on Book entity
                table.WidthPercentage = 100;

                // Headers
                string[] headers = { "ID", "Title", "Author" , "Quantity" };
                foreach (string header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }

                // Rows
                foreach (var book in books)
                {
                    table.AddCell(new Phrase(book.Id.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                    table.AddCell(new Phrase(book.Tittle, FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                    table.AddCell(new Phrase(book.Author, FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                    table.AddCell(new Phrase(book.Quantity.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                    
                }

                doc.Add(table);
                doc.Add(new Paragraph("\n"));

                // FOOTER
                Paragraph footer = new Paragraph
                {
                    Font = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.GRAY)
                };
                footer.Add(new Chunk($"Printed at: {DateTime.Now:g}"));
                footer.Add(new Chunk(new iTextSharp.text.pdf.draw.VerticalPositionMark()));
                footer.Add(new Chunk($"Printed by: {Environment.UserName}"));
                doc.Add(footer);

                doc.Close();
                writer.Close();
            }
            catch
            {
                throw;
            }
        }

    
    }
}
