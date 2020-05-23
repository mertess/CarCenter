using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class MailConfig
    {
        public string SmtpHost { set; get; }
        public int SmtpPort { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
    }
}
