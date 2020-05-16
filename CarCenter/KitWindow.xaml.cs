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

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для KitWindow.xaml
    /// </summary>
    public partial class KitWindow : Window
    {
        private readonly IKitLogic kitLogic;

        public KitWindow(IKitLogic kitLogic)
        {
            InitializeComponent();
            this.kitLogic = kitLogic;
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(KitNameTextBox.Text) && !string.IsNullOrEmpty(KitCostTextBox.Text))
                {
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
