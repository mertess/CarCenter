using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using NLog;
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
using Unity;
using Unity.Injection;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для KitsWindow.xaml
    /// </summary>
    public partial class KitsWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly IKitLogic kitLogic;
        private readonly Logger logger;

        public KitsWindow(IUnityContainer container, IKitLogic kitLogic)
        {
            InitializeComponent();
            this.container = container;
            this.kitLogic = kitLogic;
            this.logger = LogManager.GetCurrentClassLogger();
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridKits.ItemsSource = kitLogic.Read(null);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddKit_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<KitWindow>();
            window.Owner = this;
            if (window.ShowDialog().Value)
            {
                Load_Data();
            }
        }

        private void ButtonEditKit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridKits.SelectedItems.Count == 1)
            {
                if ((DataGridKits.SelectedItem as KitViewModel) != null)
                {
                    var window = container.Resolve<KitWindow>();
                    window.Owner = this;
                    window.DataContext = DataGridKits.SelectedItem as KitViewModel;
                    if (window.ShowDialog().Value)
                    {
                        Load_Data();
                    }
                }
            }else
                MessageBox.Show("Выберите одну запись!", "Сообщение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonDeleteKit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridKits.SelectedItems.Count == 1)
                {
                    if ((DataGridKits.SelectedItem as KitViewModel) != null)
                    {
                        kitLogic.Delete(DataGridKits.SelectedItem as KitViewModel);
                        Load_Data();
                    }
                }
                else
                    MessageBox.Show("Выберите одну запись!", "Сообщение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
