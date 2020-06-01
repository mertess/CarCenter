using CarCenterBusinessLogic.BusinessLogic;
using CarCenterBusinessLogic.HelperModels;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private readonly IUnityContainer unityContainer;
        private readonly Logger logger;
        public LogInWindow(IUnityContainer unityContainer)
        {
            InitializeComponent();
            this.unityContainer = unityContainer;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (!string.IsNullOrEmpty(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Text)
                    && LoginTextBox.Text.Equals(Application.Current.Resources["login"].ToString()) 
                    && PasswordTextBox.Text.Equals(Application.Current.Resources["password"].ToString()))
                {
                    MailService.SetConfig(new MailConfig()
                    {
                        SmtpHost = Application.Current.Resources["SmtpHost"].ToString(),
                        SmtpPort = Convert.ToInt32(Application.Current.Resources["SmtpPort"]),
                        Login = Application.Current.Resources["ServerEmail"].ToString(),
                        Password = Application.Current.Resources["ServerPassword"].ToString()
                    });
                    var mainWindow = unityContainer.Resolve<MainWindow>();
                    mainWindow.Show();
                    Close();
                }
                else
                    MessageBox.Show("Неверный логин или пароль!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }catch(Exception ex)
            {
                logger.Warn(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
