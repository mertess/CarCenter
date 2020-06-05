using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.BindingModels
{
    public class KitBindingModel
    {
        public int? Id { set; get; }
        public string KitName { set; get; }
        public decimal Cost { set; get; }
    }
}
