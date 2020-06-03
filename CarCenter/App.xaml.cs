using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.Interfaces;
using CarCenterImplementation.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
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

            if (!Directory.Exists("TmpFiles"))
                Directory.CreateDirectory("TmpFiles");
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            IUnityContainer container = new UnityContainer();
            SettingsContainer(container);
            container.Resolve<LogInWindow>().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (Directory.Exists("TmpFiles")) {
                foreach (var file in Directory.GetFiles("TmpFiles"))
                    File.Delete(file);
                Directory.Delete("TmpFiles");
            }
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
