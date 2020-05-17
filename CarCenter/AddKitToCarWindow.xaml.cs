using CarCenterBusinessLogic.HelperModels;
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
    /// Логика взаимодействия для AddKitToCarWindow.xaml
    /// </summary>
    public partial class AddKitToCarWindow : Window
    {
        public InstalledCarKit InstalledCarKit { set; get; }
        public AddKitToCarWindow()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if(AddKitComboBox.SelectedItem != null && !string.IsNullOrEmpty(KitCountTextBox.Text))
            {
                InstalledCarKit = new InstalledCarKit()
                {
                    KitName = (AddKitComboBox.SelectedItem as InstalledCarKit).KitName,
                    Count = Convert.ToInt32(KitCountTextBox.Text)
                };
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
