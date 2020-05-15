using CarCenterBusinessLogic.Interfaces;
using CarCenterImplementation.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace CarCenter
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICarLogic, CarLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IKitLogic, KitLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IStorageLogic, StorageLogic>(new HierarchicalLifetimeManager());
            container.Resolve<MainWindow>().Show();
        }
    }
}
