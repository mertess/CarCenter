using CarCenterBusinessLogic.Interfaces;
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

        public StoragesWindow(IUnityContainer container, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.container = container;
            this.storageLogic = storageLogic;
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridStorages.ItemsSource = storageLogic.Read(null);
            }
            catch (Exception)
            {
                //...
            }
        }

        private void ButtonAddStorage_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<StorageWindow>();
            if (window.ShowDialog().Value)
            {
                Load_Data();
            }
        }

        private void ButtonEditStorage_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridStorages.SelectedItems.Count == 1)
            {
                var window = container.Resolve<StorageWindow>();
                if (window.ShowDialog().Value)
                {
                    Load_Data();
                }
            }
        }

        private void ButtonDeleteStorage_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridStorages.SelectedItems.Count == 1)
            {
                //...
            }
        }

        private void ButtonShowStoragedKits_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridStorages.SelectedItems.Count == 1)
            {
                var window = container.Resolve<StoragedKitsWindow>();
                window.ShowDialog();
            }
        }
    }
}
