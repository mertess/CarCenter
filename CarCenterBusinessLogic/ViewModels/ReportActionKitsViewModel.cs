using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class ReportActionKitsViewModel
    {
        public DateTime DateAction { set; get; }
        public string KitName { set; get; }
        public int KitCost { set; get; }
        public int KitCount { set; get; }
        public string Action { set; get; }
    }
}
