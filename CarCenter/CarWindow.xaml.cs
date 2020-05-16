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

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {
        private readonly ICarLogic carLogic;

        public CarWindow(ICarLogic carLogic)
        {
            InitializeComponent();
            this.carLogic = carLogic;
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(CarNameTextBox.Text) && 
                    !string.IsNullOrEmpty(CarCostTextBox.Text)) {
                    if (DataContext != null)
                    {
                        carLogic.CreateOrUpdate(new CarBindingModel()
                        {
                            Id = (DataContext as CarViewModel).Id,
                            CarName = CarNameTextBox.Text,
                            Cost = Convert.ToInt32(CarCostTextBox.Text)
                        });
                    }
                    else
                    {
                        carLogic.CreateOrUpdate(new CarBindingModel()
                        {
                            CarName = CarNameTextBox.Text.ToString(),
                            Cost = Convert.ToInt32(CarCostTextBox.Text)
                        });
                    } 
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {
                //...
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
