using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class WordExcelInfo : BaseInfo
    {
        public List<ReportSoldCarViewModel> ReportList { set; get; }
    }
}
