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
        public static string Login { set; get; }
        public static string Password { set; get; }

        public static async void SendDocument<T>(T model) where T : BaseInfo
        {
            using (MailMessage mailMessage = new MailMessage("from", "to"))
            {
                mailMessage.Subject = model.Title;
                mailMessage.Body = model.Header;
                mailMessage.IsBodyHtml = true;
                //надо еще подумать
                mailMessage.Attachments.Add(new Attachment(model.Email));
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
                //TODO: брать почту и пароль из ресурсов 
                smtp.Credentials = new NetworkCredential("mitya.lagin@yandex.ru", "password");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
