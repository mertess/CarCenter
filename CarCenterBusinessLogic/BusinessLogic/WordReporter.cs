using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.ViewModels;


namespace CarCenterBusinessLogic.BusinessLogic
{
    public class WordReporter
    {   
        public static void CreateDoc(WordExcelInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.Email, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                Table table = docBody.AppendChild(new Table());
                table.AppendChild(CreateTableProperties());
/*
                foreach (var storage in info.Storages)
                {
                    table.AppendChild(new TableRow(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { storage.StorageName },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }))));
                }*/
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        //Настройка границ таблицы
        private static TableProperties CreateTableProperties()
        {
            TableProperties tblProperties = new TableProperties();

            TableBorders tblBorders = new TableBorders();
            TopBorder topBorder = new TopBorder();
            topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            topBorder.Color = "CC0000";
            tblBorders.AppendChild(topBorder);

            BottomBorder bottomBorder = new BottomBorder();
            bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            bottomBorder.Color = "CC0000";
            tblBorders.AppendChild(bottomBorder);

            RightBorder rightBorder = new RightBorder();
            rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            rightBorder.Color = "CC0000";
            tblBorders.AppendChild(rightBorder);

            LeftBorder leftBorder = new LeftBorder();
            leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            leftBorder.Color = "CC0000";
            tblBorders.AppendChild(leftBorder);

            tblProperties.AppendChild(tblBorders);

            return tblProperties;
        }

        /// Настройки страницы
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        /// Создание абзаца с текстом
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                for (int i = 0; i < paragraph.Texts.Count; i++)
                {
                    {
                        Run docRun = new Run();
                        RunProperties properties = new RunProperties();
                        properties.AppendChild(new FontSize
                        {
                            Val = paragraph.TextProperties.Size
                        });
                        if (i == 0)
                            properties.AppendChild(new Bold());
                        docRun.AppendChild(properties);
                        docRun.AppendChild(new Text
                        {
                            Text = paragraph.Texts[i],
                            Space = SpaceProcessingModeValues.Preserve
                        });
                        docParagraph.AppendChild(docRun);
                    }
                }
                return docParagraph;
            }
            return null;
        }

        /// Задание форматирования для абзаца
        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
