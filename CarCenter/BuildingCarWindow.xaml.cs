using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.BindingModels;
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
    /// Логика взаимодействия для BuildingCarWindow.xaml
    /// </summary>
    public partial class BuildingCarWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly ICarLogic carLogic;
        private readonly IBuiltCarLogic builtCarLogic;
        public List<InstalledCarKit> InstalledCarKits { set; get; }

        public BuildingCarWindow(IUnityContainer container, ICarLogic carLogic, IBuiltCarLogic builtCarLogic)
        {
            InitializeComponent();
            this.container = container;
            this.carLogic = carLogic;
            this.builtCarLogic = builtCarLogic;
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                CarComboBox.ItemsSource = carLogic.Read(null);
            }
            catch(Exception ex)
            {

            }
        }

        private void ButtonAddKit_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<AddKitToCarWindow>();
            if (window.ShowDialog().Value)
            {
                this.InstalledCarKits.Add(window.InstalledCarKit);
                Load_Data();
            }
        }

        private void ButtonEditKit_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridCarKits.SelectedItems.Count == 1)
            {
                var window = container.Resolve<AddKitToCarWindow>();
                window.InstalledCarKit = DataGridCarKits.SelectedItem as InstalledCarKit;
                if (window.ShowDialog().Value)
                {

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //I HATE ME LIFE 
            if (DataContext == null)
                Debug.WriteLine("!!!!");
            this.InstalledCarKits = builtCarLogic.Read(new BuiltCarBindingModel()
            {  
                Id = (DataContext as BuiltCarViewModel).Id
            })?[0].CarKits;
            DataGridCarKits.ItemsSource = InstalledCarKits;
        }
    }
}
