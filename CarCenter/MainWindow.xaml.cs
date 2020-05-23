using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly IBuiltCarLogic builtCarLogic;

        public MainWindow(IUnityContainer container, IBuiltCarLogic builtCarLogic)
        {
            InitializeComponent();
            this.builtCarLogic = builtCarLogic;
            this.container = container;
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridCars.ItemsSource = builtCarLogic.Read(null);
            }
            catch (Exception)
            {
                //... диалоговое окно
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs args)
        {
            var window = container.Resolve<BuildingCarWindow>();
            if (window.ShowDialog().Value)
            {
                Load_Data();
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs args)
        {
            try
            {
                if (DataGridCars.SelectedItems.Count == 1)
                {
                    var window = container.Resolve<BuildingCarWindow>();
                    window.DataContext = DataGridCars.SelectedItem as BuiltCarViewModel;
                    if (window.ShowDialog().Value)
                    {
                        Load_Data();
                    }
                }
            }catch(Exception ex)
            {
                //...
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs args)
        {
            try
            {
                if (DataGridCars.SelectedItems.Count == 1)
                {
                    builtCarLogic.Delete(DataGridCars.SelectedItem as BuiltCarViewModel);
                    Load_Data();
                }
            }catch(Exception ex)
            {
                //...
            }
        }

        private void Click_KitsMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<KitsWindow>();
            window.ShowDialog();
        }

        private void Click_StoragesMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<StoragesWindow>();
            window.ShowDialog();
        }

        private void Click_CarsMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<CarsWindow>();
            window.ShowDialog();
        }

        private void Click_ReportSoldCarsMenuItem(object sender, EventArgs args)
        {
            try
            {
                var window = container.Resolve<ReportSoldCarsWindow>();
                window.Show();
            }catch(Exception ex)
            {
                //...
            }
        }

        private void Click_ReportKitsMovingMenuItem(object sender, EventArgs args)
        {
            try
            {
                var window = container.Resolve<ReportKitsMovingWindow>();
                window.Show();
            }catch(Exception ex)
            {
                //...
            }
        }
    }
}
