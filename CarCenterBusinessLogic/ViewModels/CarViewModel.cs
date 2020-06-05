using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class CarViewModel
    {
        public int Id { set; get; }
        public string CarName { set; get; }
        public decimal Cost { set; get; }
    }
}
