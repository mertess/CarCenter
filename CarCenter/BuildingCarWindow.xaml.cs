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
using System.Collections.ObjectModel;
using CarCenterImplementation.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NLog;

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
        private readonly IKitLogic kitLogic;
        private readonly IStorageLogic storageLogic;
        private readonly Logger logger;

        public ObservableCollection<InstalledCarKit> InstalledCarKits { set; get; }

        public BuildingCarWindow(
            IUnityContainer container,
            ICarLogic carLogic,
            IBuiltCarLogic builtCarLogic,
            IKitLogic kitLogic,
            IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.container = container;
            this.carLogic = carLogic;
            this.builtCarLogic = builtCarLogic;
            this.kitLogic = kitLogic;
            this.storageLogic = storageLogic;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonAddKit_Click(object sender, RoutedEventArgs e)
        {
            var window = container.Resolve<AddKitToCarWindow>();
            window.Owner = this;
            if (window.ShowDialog().Value)
            {
                var kit = InstalledCarKits.FirstOrDefault(k => k.KitName == window.InstalledCarKit.KitName);
                if (kit != null)
                {
                    kit.Count = window.InstalledCarKit.Count;
                    kit.InstallationDate = window.InstalledCarKit.InstallationDate;
                }
                else
                    this.InstalledCarKits.Add(window.InstalledCarKit);
            }
        }

        private void ButtonEditKit_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridCarKits.SelectedItems.Count == 1)
            {
                var window = container.Resolve<AddKitToCarWindow>();
                window.Owner = this;
                window.InstalledCarKit = DataGridCarKits.SelectedItem as InstalledCarKit;
                window.ShowDialog();
            }else
                MessageBox.Show("Выберите одну запись!", "Сообщение", MessageBoxButton.OK);
        }

        private void ButtonDeleteKit_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridCarKits.SelectedItems.Count == 1)
            {
                InstalledCarKits.Remove(InstalledCarKits
                    .FirstOrDefault(ck => ck.KitName == (DataGridCarKits.SelectedItem as InstalledCarKit).KitName));
            }
            else
                MessageBox.Show("Выберите одну запись!", "Сообщение", MessageBoxButton.OK);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var cars = carLogic.Read(null);
                this.CarComboBox.ItemsSource = cars;
                if (DataContext != null)
                {
                    this.CarComboBox.SelectedItem = cars
                        .FirstOrDefault(c => c.CarName == (DataContext as BuiltCarViewModel).CarName);
                    this.InstalledCarKits = new ObservableCollection<InstalledCarKit>((DataContext as BuiltCarViewModel).CarKits);
                }
                else
                    InstalledCarKits = new ObservableCollection<InstalledCarKit>();
                DataGridCarKits.ItemsSource = InstalledCarKits;
            }
            catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var kits = kitLogic.Read(null);
                if (DataContext != null)
                {
                    builtCarLogic.CreateOrUpdate(new BuiltCarBindingModel()
                    {
                        Id = (DataContext as BuiltCarViewModel).Id,
                        CarName = (CarComboBox.SelectedItem as CarViewModel).CarName,
                        CarKits = InstalledCarKits.ToList(),
                        FinalCost = (CarComboBox.SelectedItem as CarViewModel).Cost +
                            InstalledCarKits.Sum(ck => kits.FirstOrDefault(k => k.KitName == ck.KitName).KitCost * ck.Count)
                    });
                    storageLogic.RemoveKits(new BuiltCarBindingModel() { CarKits = InstalledCarKits.ToList() });
                }
                else
                {
                    builtCarLogic.CreateOrUpdate(new BuiltCarBindingModel()
                    {
                        CarName = (CarComboBox.SelectedItem as CarViewModel).CarName,
                        CarKits = InstalledCarKits.ToList(),
                        FinalCost = (CarComboBox.SelectedItem as CarViewModel).Cost +
                           InstalledCarKits.Sum(ck => kits.FirstOrDefault(k => k.KitName == ck.KitName).KitCost * ck.Count)
                    });
                    storageLogic.RemoveKits(new BuiltCarBindingModel() { CarKits = InstalledCarKits.ToList() });
                }
                this.DialogResult = true;
                this.Close();
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
