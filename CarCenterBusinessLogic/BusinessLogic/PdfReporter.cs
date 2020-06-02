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
            Paragraph paragraphTitle = section.AddParagraph();
            paragraphTitle.AddFormattedText(info.Title, TextFormat.Bold);
            paragraphTitle.Format.SpaceAfter = "1cm";
            paragraphTitle.Format.Alignment = ParagraphAlignment.Center;
            Paragraph paragraphBody = section.AddParagraph(info.Body);
            paragraphBody.Format.SpaceAfter = "1cm";
            paragraphBody.Format.Alignment = ParagraphAlignment.Center;
            paragraphBody.Style = "Normal";
            Paragraph paragraphPeriod = section.AddParagraph($"Период с {info.DateFrom} по {info.DateTo}");
            paragraphPeriod.Format.SpaceAfter = "1cm";
            paragraphPeriod.Format.Alignment = ParagraphAlignment.Left;
            paragraphPeriod.Style = "Normal";

            var table = document.LastSection.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            List<string> columns = new List<string> { "3cm", "4cm", "3cm", "3cm", "3cm" };
            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "Дата", "Комплектация", "Стоимость за шт.", "Действие", "Количество"},
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            foreach (var kit in info.ReportActionKits)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> { kit.DateAction.ToShortDateString(), kit.KitName,
                        kit.KitCost.ToString(), kit.Action, kit.KitCount.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FilePath);
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
