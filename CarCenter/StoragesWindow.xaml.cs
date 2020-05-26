using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
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
using Unity;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для StoragesWindow.xaml
    /// </summary>
    public partial class StoragesWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly IStorageLogic storageLogic;
        private readonly Logger logger;

        public StoragesWindow(IUnityContainer container, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.container = container;
            this.storageLogic = storageLogic;
            this.logger = LogManager.GetCurrentClassLogger();
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridStorages.ItemsSource = storageLogic.Read(null);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void ButtonShowStoragedKits_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridStorages.SelectedItems.Count == 1)
            {
                var window = container.Resolve<StoragedKitsWindow>();
                window.DataGridStoragedKits.ItemsSource = (DataGridStorages.SelectedItem as StorageViewModel).StoragedKits;
                window.Show();
            }else
                MessageBox.Show("Выберите одну запись!", "Сообщение", MessageBoxButton.OK);
        }
    }
}
