using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using CarCenterBusinessLogic.HelperModels;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public static class MailService
    {
        private static string SmtpHost { set; get; }
        private static int SmtpPort { set; get; }
        private static string Login { set; get; }
        private static string Password { set; get; }

        public static void SetConfig(MailConfig mailConfig)
        {
            SmtpHost = mailConfig.SmtpHost;
            SmtpPort = mailConfig.SmtpPort;
            Login = mailConfig.Login;
            Password = mailConfig.Password;
        }

        public static async void SendDocumentAsync(MailSendInfo info) 
        {
            using (MailMessage mailMessage = new MailMessage(Login, info.Email))
            {
               using(SmtpClient smtpClient = new SmtpClient(SmtpHost, SmtpPort))
               {
                    mailMessage.Subject = info.Title;
                    mailMessage.Body = info.Body;
                    mailMessage.Attachments.Add(new Attachment(info.FilePath));
                    smtpClient.Credentials = new NetworkCredential(Login, Password);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(mailMessage);
               }
            }
        }
    }
}
