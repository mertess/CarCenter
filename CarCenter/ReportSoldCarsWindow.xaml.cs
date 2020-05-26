using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.HelperModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для ReportSoldCarsWindow.xaml
    /// </summary>
    public partial class ReportSoldCarsWindow : Window
    {
        private readonly ReportLogic reportLogic;
        private readonly Logger logger;
        private readonly string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        public ReportSoldCarsWindow(ReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonSendDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EmailDocTextBox.Text) && Regex.IsMatch(EmailDocTextBox.Text, pattern))
                {
                    reportLogic.SendWordReport(EmailDocTextBox.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Поле ввода адреса электронной почты не заполнено \n" +
                    "\t\tили не верный формат!", "Предупреждение", MessageBoxButton.OK);
            }
            catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void ButtonSendXls_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EmailExcelTextBox.Text) && Regex.IsMatch(EmailExcelTextBox.Text, pattern))
                {
                    reportLogic.SendExcelReport(EmailExcelTextBox.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Поле ввода адреса электронной почты не заполнено \n" +
                    "\t\tили не верный формат!", "Предупреждение", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
