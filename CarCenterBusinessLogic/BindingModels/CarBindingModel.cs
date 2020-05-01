using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.BindingModels
{
    public class CarBindingModel
    {
        public int Id { set; get; }
        public string CarName { set; get; }
        public int Cost { set; get; }
        public DateTime? SoldDate { set; get; }
        //key = kitname, value = (count, InstallationDate) 
        public Dictionary<string, (int, DateTime?)> CarKits { set; get; }
    }
}
