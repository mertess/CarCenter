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
    /// Логика взаимодействия для KitWindow.xaml
    /// </summary>
    public partial class KitWindow : Window
    {
        private readonly IKitLogic kitLogic;
        private readonly Logger logger;

        public KitWindow(IKitLogic kitLogic)
        {
            InitializeComponent();
            this.kitLogic = kitLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(KitNameTextBox.Text) && !string.IsNullOrEmpty(KitCostTextBox.Text))
                {
                    if (KitCostTextBox.Text.TrimStart().StartsWith("-"))
                    {
                        MessageBox.Show("Стоимость не может быть отрицательной!", "Предупреждение",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (this.DataContext != null)
                    {
                        kitLogic.CreateOrUpdate(new KitBindingModel()
                        {
                            Id = (DataContext as KitViewModel).Id,
                            KitName = KitNameTextBox.Text,
                            Cost = Convert.ToInt32(KitCostTextBox.Text)
                        });
                    }
                    else
                    {
                        kitLogic.CreateOrUpdate(new KitBindingModel()
                        {
                            KitName = KitNameTextBox.Text,
                            Cost = Convert.ToInt32(KitCostTextBox.Text)
                        });
                    }
                    this.DialogResult = true;
                    this.Close();
                }else
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
