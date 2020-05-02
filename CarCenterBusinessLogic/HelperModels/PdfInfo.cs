using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string Email { set; get; }
        public string Title { set; get; }
        public string Header { set; get; }
        public List<ReportActionKitsViewModel> ReportActionKits { set; get; }
    }
}
