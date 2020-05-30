using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterImplementation.Models;
using NLog;
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
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для AddKitToCarWindow.xaml
    /// </summary>
    public partial class AddKitToCarWindow : Window
    {
        private readonly IKitLogic kitLogic;
        private readonly IStorageLogic storageLogic;
        private readonly Logger logger;
        public InstalledCarKit InstalledCarKit { set; get; }

        public AddKitToCarWindow(IKitLogic kitLogic, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.kitLogic = kitLogic;
            this.storageLogic = storageLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddKitComboBox.SelectedItem != null && !string.IsNullOrEmpty(KitCountTextBox.Text)) 
                {
                    if (KitCountTextBox.Text.TrimStart().StartsWith("-"))
                    {
                        MessageBox.Show("Количество не может быть отрицательным!", "Предупреждение", MessageBoxButton.OK);
                        return;
                    }
                    if (storageLogic.CheckCountKits(new InstalledCarKit()
                    {
                        KitName = (AddKitComboBox.SelectedItem as KitViewModel).KitName,
                        Count = Convert.ToInt32(KitCountTextBox.Text) - InstalledCarKit.Count
                    }))
                    {
                        InstalledCarKit.KitName = (AddKitComboBox.SelectedItem as KitViewModel).KitName;
                        InstalledCarKit.Count = Convert.ToInt32(KitCountTextBox.Text);
                        InstalledCarKit.InstallationDate = DateTime.Now;
                        InstalledCarKit.RemovedFromStorages = false;
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Недостаточно данной комплектации на скадах!", "Предупреждение", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Заполнены не все поля!", "Предупреждение", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
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
            }
            catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
