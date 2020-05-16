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
using System.Windows.Forms;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для StorageWindow.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        private readonly IStorageLogic storageLogic;

        public StorageWindow(IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.storageLogic = storageLogic;
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(StorageNameTextBox.Text))
                {
                    if (this.DataContext != null)
                    {
                        storageLogic.CreateOrUpdate(new StorageBindingModel()
                        {
                            Id = (DataContext as StorageViewModel).Id,
                            StorageName = StorageNameTextBox.Text
                        });
                    }
                    else
                    {
                        storageLogic.CreateOrUpdate(new StorageBindingModel()
                        {
                            StorageName = StorageNameTextBox.Text
                        });
                    }
                }
                this.DialogResult = true;
                this.Close();
            }catch(Exception ex)
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
