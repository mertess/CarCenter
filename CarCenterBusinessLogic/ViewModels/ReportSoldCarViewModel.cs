using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class ReportSoldCarViewModel
    {
        public DateTime SoldDate { set; get; }
        public string CarName { set; get; }
        public decimal CarCost { set; get; }
        //key = kitname, value = (cost, count) 
        public Dictionary<string, (decimal, int)> CarKits { set; get; }
    }
}
