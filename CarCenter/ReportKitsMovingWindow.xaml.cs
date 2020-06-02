using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NLog;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для ReportKitsMovingWindow.xaml
    /// </summary>
    public partial class ReportKitsMovingWindow : Window
    {
        private readonly ReportLogic reportLogic;
        private readonly Logger logger;

        public ReportKitsMovingWindow(ReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonMakeReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DatePickerFrom.SelectedDate.HasValue && DatePickerTo.SelectedDate.HasValue
                    && DatePickerFrom.SelectedDate <= DatePickerTo.SelectedDate)
                {
                    if (TableRowGroup.Rows.Count != 1) TableRowGroup.Rows.RemoveRange(1, TableRowGroup.Rows.Count - 1);
                    CreateTable(reportLogic.GetReportActionKits(new KitReportPeriodBindingModel()
                    {
                        DateFrom = DatePickerFrom.SelectedDate.Value,
                        DateTo = DatePickerTo.SelectedDate.Value
                    }));
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать временной период!" + 
                        " Его начало не должно превышать его конец!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateTable(List<ReportActionKitsViewModel> reportActionKitsViews)
        {
            foreach(var kit in reportActionKitsViews)
            {
                TableRow tableRow = new TableRow();
                TableCell cellDate = new TableCell(new Paragraph(new Run(kit.DateAction.ToShortDateString())))
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1)
                };
                tableRow.Cells.Add(cellDate);

                TableCell cellKitName = new TableCell(new Paragraph(new Run(kit.KitName)))
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1)
                };
                tableRow.Cells.Add(cellKitName);

                TableCell cellKitCost = new TableCell(new Paragraph(new Run(kit.KitCost.ToString())))
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1)
                };
                tableRow.Cells.Add(cellKitCost);

                TableCell cellKitAction = new TableCell(new Paragraph(new Run(kit.Action)))
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1)
                };
                tableRow.Cells.Add(cellKitAction);

                TableCell cellKitCount = new TableCell(new Paragraph(new Run(kit.KitCount.ToString())))
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1)
                };
                tableRow.Cells.Add(cellKitCount);

                TableRowGroup.Rows.Add(tableRow);
            }
        }

        private void ButtonSendPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DatePickerFrom.SelectedDate.HasValue && DatePickerTo.SelectedDate.HasValue
                    && DatePickerFrom.SelectedDate <= DatePickerTo.SelectedDate)
                {
                    var window = new InputClientEmailWindow();
                    window.Owner = this;
                    if (window.ShowDialog().Value)
                    {
                        reportLogic.SendPdfReport(window.EmailPdfTextBox.Text,
                            DatePickerFrom.SelectedDate.Value, DatePickerTo.SelectedDate.Value);
                    }
                }
                else
                    MessageBox.Show("Необходимо выбрать временной период!" +
                        " Его начало не должно превышать его конец!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
