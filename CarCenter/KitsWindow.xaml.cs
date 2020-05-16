using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
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
    /// Логика взаимодействия для KitsWindow.xaml
    /// </summary>
    public partial class KitsWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly IKitLogic kitLogic;

        public KitsWindow(IUnityContainer container, IKitLogic kitLogic)
        {
            InitializeComponent();
            this.container = container;
            this.kitLogic = kitLogic;
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridKits.ItemsSource = kitLogic.Read(null);
            }
            catch (Exception)
            {

            }
        }

        private void ButtonAddKit_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<KitWindow>();
            if (window.ShowDialog().Value)
            {
                Load_Data();
            }
        }

        private void ButtonEditKit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridKits.SelectedItems.Count == 1)
            {
                var window = container.Resolve<KitWindow>();
                window.DataContext = DataGridKits.SelectedItem as KitViewModel;
                if (window.ShowDialog().Value)
                {
                    Load_Data();
                }
            }
        }

        private void ButtonDeleteKit_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridKits.SelectedItems.Count == 1)
            {
                kitLogic.Delete(DataGridKits.SelectedItem as KitViewModel);
                Load_Data();
            }
        }
    }
}
