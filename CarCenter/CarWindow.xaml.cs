using CarCenterBusinessLogic.Interfaces;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using NLog;
using System.Text.RegularExpressions;
using System.Globalization;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {
        private readonly ICarLogic carLogic;
        private readonly Logger logger;

        public CarWindow(ICarLogic carLogic)
        {
            InitializeComponent();
            this.carLogic = carLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(CarNameTextBox.Text) && 
                    !string.IsNullOrEmpty(CarCostTextBox.Text)) {
                    if (CarCostTextBox.Text.TrimStart().StartsWith("-"))
                    {
                        MessageBox.Show("Стоимость не может быть отрицательной!", "Предупреждение",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (DataContext != null)
                    {
                        carLogic.CreateOrUpdate(new CarBindingModel()
                        {
                            Id = (DataContext as CarViewModel).Id,
                            CarName = CarNameTextBox.Text,
                            Cost = Decimal.Parse(CarCostTextBox.Text, CultureInfo.InvariantCulture)
                        });
                    }
                    else
                    {
                        carLogic.CreateOrUpdate(new CarBindingModel()
                        {
                            CarName = CarNameTextBox.Text.ToString(),
                            Cost = Decimal.Parse(CarCostTextBox.Text, CultureInfo.InvariantCulture)
                        });
                    }
                    this.DialogResult = true;
                    this.Close();
                }
                else
                    MessageBox.Show("Заполните все поля!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
