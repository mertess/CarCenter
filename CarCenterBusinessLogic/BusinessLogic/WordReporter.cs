using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.ViewModels;
using System.Linq;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public class WordReporter
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FilePath, WordprocessingDocumentType.Document))
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
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Body },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = false,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                Table table = docBody.AppendChild(new Table());
                table.AppendChild(CreateTableProperties());
                table.AppendChild(CreateHeaderRow());
                foreach (var sc in info.ReportSoldCars)
                {
                    TableRow tableRow = new TableRow();
                    tableRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sc.SoldDate.ToShortDateString() },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    })));
                    tableRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sc.CarName },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    })));
                    tableRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sc.CarCost.ToString() },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    })));
                    tableRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sc.CarKits.Keys.FirstOrDefault() },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    })));
                    tableRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sc.CarKits.Values.FirstOrDefault().Item1.ToString() },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    })));
                    tableRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sc.CarKits.Values.FirstOrDefault().Item2.ToString() },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    })));
                    table.AppendChild(tableRow);
                    foreach (var kit in sc.CarKits.Skip(1))
                    {
                        TableRow kitRow = new TableRow();
                        for (int i = 0; i < 3; i++)
                            kitRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                            {
                                Texts = new List<string> { "" },
                                TextProperties = new WordParagraphProperties
                                {
                                    Bold = false,
                                    Size = "24",
                                    JustificationValues = JustificationValues.Center
                                }
                            })));

                        kitRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { kit.Key },
                            TextProperties = new WordParagraphProperties
                            {
                                Bold = false,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        })));
                        kitRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { kit.Value.Item1.ToString() },
                            TextProperties = new WordParagraphProperties
                            {
                                Bold = false,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        })));
                        kitRow.AppendChild(new TableCell(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { kit.Value.Item2.ToString() },
                            TextProperties = new WordParagraphProperties
                            {
                                Bold = false,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        })));
                        table.AppendChild(kitRow);
                    }
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static TableRow CreateHeaderRow()
        {
            TableRow tableRowHeader = new TableRow();
            tableRowHeader.AppendChild(new TableCell(CreateParagraph(new WordParagraph
            {
                Texts = new List<string> { "Дата" },
                TextProperties = new WordParagraphProperties
                {
                    Bold = false,
                    Size = "24",
                    JustificationValues = JustificationValues.Center
                }
            })));
            tableRowHeader.AppendChild(new TableCell(CreateParagraph(new WordParagraph
            {
                Texts = new List<string> { "Машина" },
                TextProperties = new WordParagraphProperties
                {
                    Bold = false,
                    Size = "24",
                    JustificationValues = JustificationValues.Center
                }
            })));
            tableRowHeader.AppendChild(new TableCell(CreateParagraph(new WordParagraph
            {
                Texts = new List<string> { "Стоимость машины" },
                TextProperties = new WordParagraphProperties
                {
                    Bold = false,
                    Size = "24",
                    JustificationValues = JustificationValues.Center
                }
            })));
            tableRowHeader.AppendChild(new TableCell(CreateParagraph(new WordParagraph
            {
                Texts = new List<string> { "Комплектация" },
                TextProperties = new WordParagraphProperties
                {
                    Bold = false,
                    Size = "24",
                    JustificationValues = JustificationValues.Center
                }
            })));
            tableRowHeader.AppendChild(new TableCell(CreateParagraph(new WordParagraph
            {
                Texts = new List<string> { "Стоимость за шт." },
                TextProperties = new WordParagraphProperties
                {
                    Bold = false,
                    Size = "24",
                    JustificationValues = JustificationValues.Center
                }
            })));
            tableRowHeader.AppendChild(new TableCell(CreateParagraph(new WordParagraph
            {
                Texts = new List<string> { "Количество" },
                TextProperties = new WordParagraphProperties
                {
                    Bold = false,
                    Size = "24",
                    JustificationValues = JustificationValues.Center
                }
            })));

            return tableRowHeader;
        }

        //Настройка границ таблицы
        private static TableProperties CreateTableProperties()
        {
            TableProperties tblProperties = new TableProperties();

            TableBorders tblBorders = new TableBorders();
            TopBorder topBorder = new TopBorder();
            topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            topBorder.Color = "000000";
            tblBorders.AppendChild(topBorder);

            BottomBorder bottomBorder = new BottomBorder();
            bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            bottomBorder.Color = "000000";
            tblBorders.AppendChild(bottomBorder);

            RightBorder rightBorder = new RightBorder();
            rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            rightBorder.Color = "000000";
            tblBorders.AppendChild(rightBorder);

            LeftBorder leftBorder = new LeftBorder();
            leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            leftBorder.Color = "000000";
            tblBorders.AppendChild(leftBorder);

            InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();
            insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            insideHBorder.Color = "000000";
            tblBorders.AppendChild(insideHBorder);

            InsideVerticalBorder insideVBorder = new InsideVerticalBorder();
            insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            insideVBorder.Color = "000000";
            tblBorders.AppendChild(insideVBorder);

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
                        if (paragraph.TextProperties.Bold == true)
                            properties.Bold = new Bold();
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
