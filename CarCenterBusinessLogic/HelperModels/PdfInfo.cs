using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class PdfInfo : BaseInfo
    {
        public List<ReportActionKitsViewModel> ReportActionKits { set; get; }
    }
}
