using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.BindingModels
{
    public class CarBindingModel
    {
        public int? Id { set; get; }
        public string CarName { set; get; }
        public decimal Cost { set; get; }
    }
}
