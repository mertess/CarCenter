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
        public DateTime? SoldDate { set; get; }
        public int Cost { set; get; }
        public Dictionary<string, (int, DateTime)> CarKits { set; get; }
    }
}
