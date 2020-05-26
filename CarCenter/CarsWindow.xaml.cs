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

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для CarsWindow.xaml
    /// </summary>
    public partial class CarsWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly ICarLogic carLogic;
        private readonly Logger logger;

        public CarsWindow(IUnityContainer container, ICarLogic carLogic)
        {
            InitializeComponent();
            this.container = container;
            this.carLogic = carLogic;
            this.logger = LogManager.GetCurrentClassLogger();
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridCars.ItemsSource = carLogic.Read(null);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void ButtonAddCar_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<CarWindow>();
            if (window.ShowDialog().Value)
            {
                Load_Data();
            }
        }

        private void ButtonEditCar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridCars.SelectedItems.Count == 1)
            {
                var window = container.Resolve<CarWindow>();
                window.DataContext = DataGridCars.SelectedItem as CarViewModel;
                if (window.ShowDialog().Value)
                {
                    Load_Data();
                }
            }else
                MessageBox.Show("Выберите одну запись!", "Сообщение", MessageBoxButton.OK);
        }

        private void ButtonDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridCars.SelectedItems.Count == 1)
                {
                    carLogic.Delete(DataGridCars.SelectedItem as CarViewModel);
                    Load_Data();
                }
                else
                    MessageBox.Show("Выберите одну запись!", "Сообщение", MessageBoxButton.OK);
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
