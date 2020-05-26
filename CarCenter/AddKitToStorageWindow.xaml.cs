using CarCenterBusinessLogic.Interfaces;
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
using CarCenterBusinessLogic.ViewModels;
using NLog;
using System.Text.RegularExpressions;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для AddKitToStorageWindow.xaml
    /// </summary>
    public partial class AddKitToStorageWindow : Window
    {
        private readonly IKitLogic kitLogic;
        private readonly IStorageLogic storageLogic;
        private readonly Logger logger;

        public AddKitToStorageWindow(IKitLogic kitLogic, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.kitLogic = kitLogic;
            this.storageLogic = storageLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StorageComboBox.SelectedItem != null && KitComboBox.SelectedItem != null &&
                    !string.IsNullOrEmpty(KitCountTextBox.Text)) {
                    storageLogic.AddKitToStorage(new DepositKitBindingModel()
                    {
                        StorageId = (StorageComboBox.SelectedItem as StorageViewModel).Id,
                        KitId = (KitComboBox.SelectedValue as KitViewModel).Id,
                        KitCount = Convert.ToInt32(KitCountTextBox.Text)
                    });
                    Close();
                }
                else
                    MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButton.OK);
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.StorageComboBox.ItemsSource = storageLogic.Read(null);
                this.KitComboBox.ItemsSource = kitLogic.Read(null);
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
