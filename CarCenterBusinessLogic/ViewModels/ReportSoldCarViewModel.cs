using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class ReportSoldCarViewModel
    {
        public DateTime SoldDate { set; get; }
        public string CarName { set; get; }
        public int CarCost { set; get; }
        //key = kitname, value = (cost, count) 
        public Dictionary<string, (int, int)> CarKits { set; get; }
    }
}
