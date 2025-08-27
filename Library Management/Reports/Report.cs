using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;

namespace Library_Management.Reports
{
    public class Report
    {
        public static void ExportGridToPDF(DataGridView dgv, string pdfPath, string reportTitle,
            DateTime startDate, DateTime endDate, float[] columnWidths, string logoPath = null)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            try
            {
                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                doc.Open();

                // ===== HEADER: Logo + Title =====
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 4f });

                // Logo
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
                    PdfPCell emptyCell = new PdfPCell(new Phrase(""))
                    {
                        Border = Rectangle.NO_BORDER
                    };
                    headerTable.AddCell(emptyCell);
                }

                // Title
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                PdfPCell titleCell = new PdfPCell(new Phrase(reportTitle, titleFont))
                {
                    Border = Rectangle.NO_BORDER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                headerTable.AddCell(titleCell);

                doc.Add(headerTable);
                doc.Add(new Paragraph("\n"));

                // ===== START & END DATES =====
                PdfPTable dateTable = new PdfPTable(2);
                dateTable.WidthPercentage = 50;
                dateTable.SetWidths(new float[] { 1f, 1f });

                PdfPCell startCell = new PdfPCell(new Phrase("Start Date: " + startDate.ToShortDateString()))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                dateTable.AddCell(startCell);

                PdfPCell endCell = new PdfPCell(new Phrase("End Date: " + endDate.ToShortDateString()))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                dateTable.AddCell(endCell);

                doc.Add(dateTable);
                doc.Add(new Paragraph("\n"));

                // ===== GRID DATA =====
                PdfPTable table = new PdfPTable(dgv.Columns.Count);
                table.WidthPercentage = 100;
                if (columnWidths != null && columnWidths.Length == dgv.Columns.Count)
                    table.SetWidths(columnWidths);

                // Table headers
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }

                // Table rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            PdfPCell dataCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 9)))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT
                            };
                            table.AddCell(dataCell);
                        }
                    }
                }

                doc.Add(table);
                doc.Add(new Paragraph("\n"));

                // ===== FOOTER =====
                string footerText = $"Printed at: {DateTime.Now:g}    Printed by: {Environment.UserName}";
                Paragraph footer = new Paragraph(footerText, FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.GRAY))
                {
                    Alignment = Element.ALIGN_LEFT
                };
                doc.Add(footer);

                doc.Close();
                writer.Close();

                MessageBox.Show("PDF exported successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting PDF: " + ex.Message);
            }
        }
    }
}
