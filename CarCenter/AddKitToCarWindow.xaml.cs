using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterImplementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        private readonly IKitLogic kitLogic;
        public InstalledCarKit InstalledCarKit { set; get; }
        public AddKitToCarWindow(IKitLogic kitLogic)
        {
            InitializeComponent();
            this.kitLogic = kitLogic;
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
                InstalledCarKit.KitName = (AddKitComboBox.SelectedItem as KitViewModel).KitName;
                InstalledCarKit.Count = Convert.ToInt32(KitCountTextBox.Text);
                InstalledCarKit.InstallationDate = DateTime.Now;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                //...
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var kits = kitLogic.Read(null);
                this.AddKitComboBox.ItemsSource = kits;
                if (InstalledCarKit != null)
                {
                    this.AddKitComboBox.SelectedItem = kits.FirstOrDefault(k => k.KitName == InstalledCarKit.KitName);
                    this.KitCountTextBox.Text = InstalledCarKit.Count.ToString();
                }
                else
                    InstalledCarKit = new InstalledCarKit();
            }catch(Exception ex)
            {
                //...
            }
        }
    }
}
