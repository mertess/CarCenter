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

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для InputClientEmailWindow.xaml
    /// </summary>
    public partial class InputClientEmailWindow : Window
    {
        public InputClientEmailWindow()
        {
            InitializeComponent();
        }

        private void ButtonSendPdf_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailPdfTextBox.Text))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                //...
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
