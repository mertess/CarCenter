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
using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.Enums;
using CarCenterBusinessLogic.BusinessLogic;
using NLog;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUnityContainer container;
        private readonly IBuiltCarLogic builtCarLogic;
        private readonly ICarLogic carLogic;
        private readonly BackUpAbstractLogic backUpLogic;
        private readonly Logger logger;

        public MainWindow(IUnityContainer container, IBuiltCarLogic builtCarLogic,
            ICarLogic carLogic, BackUpAbstractLogic backUpLogic)
        {
            InitializeComponent();
            this.builtCarLogic = builtCarLogic;
            this.container = container;
            this.carLogic = carLogic;
            this.backUpLogic = backUpLogic;
            this.logger = LogManager.GetCurrentClassLogger();
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                DataGridCars.ItemsSource = builtCarLogic.Read(null);
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs args)
        {
            try
            {
                if (carLogic.Read(null).Count != 0)
                {
                    var window = container.Resolve<BuildingCarWindow>();
                    window.Owner = this;
                    if (window.ShowDialog().Value)
                    {
                        Load_Data();
                    }
                }else
                    MessageBox.Show("Добавьте хотябы одну машину!", "Сообщение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs args)
        {
            if (DataGridCars.SelectedItems.Count == 1)
            {
                if ((DataGridCars.SelectedItem as BuiltCarViewModel) != null)
                {
                    if ((DataGridCars.SelectedItem as BuiltCarViewModel).SoldDate == null)
                    {
                        var window = container.Resolve<BuildingCarWindow>();
                        window.Owner = this;
                        window.DataContext = DataGridCars.SelectedItem as BuiltCarViewModel;
                        if (window.ShowDialog().Value)
                        {
                            Load_Data();
                        }
                    }
                    else
                        MessageBox.Show("Данная машина уже продана!", "Сообщение",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
                MessageBox.Show("Выберите одну запись!", "Сообщение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonDelete_Click(object sender, EventArgs args)
        {
            try
            {
                if (DataGridCars.SelectedItems.Count == 1)
                {
                    if ((DataGridCars.SelectedItem as BuiltCarViewModel) != null)
                    {
                        if ((DataGridCars.SelectedItem as BuiltCarViewModel).SoldDate == null)
                        {
                            builtCarLogic.Delete(DataGridCars.SelectedItem as BuiltCarViewModel);
                            Load_Data();
                        }
                        else
                            MessageBox.Show("Данная машина уже продана!", "Сообщение",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                    MessageBox.Show("Выберите одну запись!", "Сообщение", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_KitsMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<KitsWindow>();
            window.Owner = this;
            window.ShowDialog();
        }

        private void Click_StoragesMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<StoragesWindow>();
            window.Owner = this;
            window.ShowDialog();
        }

        private void Click_CarsMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<CarsWindow>();
            window.Owner = this;
            window.ShowDialog();
        }

        private void Click_ReportSoldCarsMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<ReportSoldCarsWindow>();
            window.Owner = this;
            window.Show();
        }

        private void Click_ReportKitsMovingMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<ReportKitsMovingWindow>();
            window.Owner = this;
            window.Show();
        }

        private void Click_AddKitToStorageMenuItem(object sender, EventArgs args)
        {
            var window = container.Resolve<AddKitToStorageWindow>();
            window.Owner = this;
            window.Show();
        }

        private void Click_SellCarMenuItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridCars.SelectedItems.Count == 1)
                {
                    if ((DataGridCars.SelectedItem as BuiltCarViewModel) != null)
                    {
                        if ((DataGridCars.SelectedItem as BuiltCarViewModel).SoldDate == null)
                        {
                            builtCarLogic.CreateOrUpdate(new BuiltCarBindingModel()
                            {
                                Id = (DataGridCars.SelectedItem as BuiltCarViewModel).Id,
                                SoldDate = DateTime.Now,
                                FinalCost = (DataGridCars.SelectedItem as BuiltCarViewModel).FinalCost
                            });
                            Load_Data();
                        }
                        else
                            MessageBox.Show("Данная машина уже продана!", "Сообщение",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                    MessageBox.Show("Выберите одну запись!", "Сообщение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_SaveJsonBackUpMenuItem(object sender, EventArgs args)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    backUpLogic.CreateDir(dialog.SelectedPath, BackUpTypeEnum.Json);
                }
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_SaveXmlBackUpMenuItem(object sender, EventArgs args)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    backUpLogic.CreateDir(dialog.SelectedPath, BackUpTypeEnum.Xml);
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_LoadJsonBackUpMenuItem(object sender, EventArgs args)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                MessageBox.Show("Внимание!\nДанная операция может привести к потери текущих данных!",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    backUpLogic.LoadBackUp(dialog.SelectedPath, BackUpTypeEnum.Json);
                    Load_Data();
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_LoadXmlBackUpMenuItem(object sender, EventArgs args)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                MessageBox.Show("Внимание!\nДанная операция может привести к потери текущих данных!",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    backUpLogic.LoadBackUp(dialog.SelectedPath, BackUpTypeEnum.Xml);
                    Load_Data();
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
