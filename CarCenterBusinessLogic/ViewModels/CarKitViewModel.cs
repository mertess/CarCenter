using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class CarKitViewModel
    {
        public int KitId { set; get; }
        public string KitName { set; get; }
        public string CarName { set; get; }
        public int KitCost { set; get; }
        public int KitCount { set; get; }
        public DateTime InstallationDate { set; get; }
    }
}
