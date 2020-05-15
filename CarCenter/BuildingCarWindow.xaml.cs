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
    /// Логика взаимодействия для BuildingCarWindow.xaml
    /// </summary>
    public partial class BuildingCarWindow : Window
    {
        private readonly IUnityContainer container;

        public BuildingCarWindow(IUnityContainer container)
        {
            InitializeComponent();
            this.container = container;
        }

        private void Load_Data()
        {
            //...
        }

        private void ButtonAddKit_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<AddKitToCarWindow>();
            if (window.ShowDialog().Value)
            {
                Load_Data();
            }
        }

        private void ButtonEditKit_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridCarKits.SelectedItems.Count == 1)
            {
                var window = container.Resolve<AddKitToCarWindow>();
                if (window.ShowDialog().Value)
                {
                    Load_Data();
                }
            }
        }

        private void ButtonDeleteKit_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridCarKits.SelectedItems.Count == 1)
            {
                //...
            }
        }
    }
}
