using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FilePath { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public string DateFrom { set; get; }
        public string DateTo { set; get; }
        public List<ReportActionKitsViewModel> ReportActionKits { set; get; }
    }
}
