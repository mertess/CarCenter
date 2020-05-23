using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.HelperModels;
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
            SettingsContainer(container);
            MailService.SetConfig(new MailConfig()
            {
                Login = "mitya.lagin@yandex.ru",
                Password = "56321cegawdemalagin2019",
                SmtpHost = "smtp.yandex.ru",
                SmtpPort = 25
            });
            container.Resolve<MainWindow>().Show();
        }

        private void SettingsContainer(IUnityContainer container)
        {
            container.RegisterType<ICarLogic, CarLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IKitLogic, KitLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IStorageLogic, StorageLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IBuiltCarLogic, BuiltCarLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportHelper, ReportHelper>(new HierarchicalLifetimeManager());
        }
    }
}
