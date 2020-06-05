using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class DepositKitViewModel
    {
        public int KitId { set; get; }
        public string KitName { set; get; }
        public int KitCount { set; get; }
        public decimal KitCost { set; get; }
        public DateTime DepositDate { set; get; }
    }
}
