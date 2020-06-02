using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.BindingModels
{
    public class DepositKitBindingModel
    {
        public int KitId { set; get; }
        public int StorageId { set; get; }
        public int KitCount { set; get; }
    }
}
