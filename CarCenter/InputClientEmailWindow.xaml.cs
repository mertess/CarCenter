using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для InputClientEmailWindow.xaml
    /// </summary>
    public partial class InputClientEmailWindow : Window
    {
        private readonly string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public InputClientEmailWindow()
        {
            InitializeComponent();
        }

        private void ButtonSendPdf_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailPdfTextBox.Text) && Regex.IsMatch(EmailPdfTextBox.Text, pattern))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Поле ввода адреса электронной почты не заполнено \n" +
                    "\t\tили не верный формат!", "Предупреждение", MessageBoxButton.OK);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
