using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class WordExcelInfo
    {
        public string Email { set; get; }
        public string Title { set; get; }
        public string Header { set; get; }
        public List<ReportSoldCarViewModel> ReportSoldCars { set; get; }
    }
}
