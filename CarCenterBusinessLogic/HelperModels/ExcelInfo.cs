using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FilePath { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public List<ReportSoldCarViewModel> ReportSoldCars { set; get; }
    }
}
