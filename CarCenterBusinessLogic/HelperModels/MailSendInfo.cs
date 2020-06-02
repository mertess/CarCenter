using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class MailSendInfo
    {
        public string Email { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public string FilePath { set; get; }
    }
}
