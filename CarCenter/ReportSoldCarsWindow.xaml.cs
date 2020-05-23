using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для ReportSoldCarsWindow.xaml
    /// </summary>
    public partial class ReportSoldCarsWindow : Window
    {
        private readonly ReportLogic reportLogic;

        public ReportSoldCarsWindow(ReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
        }

        private void ButtonSendDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportLogic.SendWordReport(EmailDocTextBox.Text);
                this.Close();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ButtonSendXls_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportLogic.SendExcelReport(EmailExcelTextBox.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                //...
            }
        }
    }
}
