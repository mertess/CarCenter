using System;
using System.Collections.Generic;
using System.Text;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using CarCenterBusinessLogic.HelperModels;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public class PdfReporter
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "Normal";
            var table = document.LastSection.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            List<string> columns = new List<string> { "3cm", "3cm", "3cm" };
            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "Платье", "Материал", "Количество"},
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            /*foreach (var dress in info.Dresses)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> { dress.DressName, dress.MaterialName, dress.MaterialCount.ToString()},
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
            }*/
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.Email);
        }

        // Создание стилей для документа
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }

        // Создание и заполнение строки
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }

        // Заполнение ячейки
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
