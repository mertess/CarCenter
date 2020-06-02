using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.Interfaces;
using CarCenterImplementation.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
            SettingsContainer(container);
            container.Resolve<LogInWindow>().Show();
        }

        private void SettingsContainer(IUnityContainer container)
        {
            container.RegisterType<ICarLogic, CarLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IKitLogic, KitLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IStorageLogic, StorageLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IBuiltCarLogic, BuiltCarLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportHelper, ReportHelper>(new HierarchicalLifetimeManager());
            container.RegisterType<BackUpAbstractLogic, BackUpLogic>(new HierarchicalLifetimeManager());
        }
    }
}
