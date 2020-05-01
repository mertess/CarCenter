using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class CarViewModel
    {
        public int Id { set; get; }
        [DisplayName("Машина")]
        public string CarName { set; get; }
        [DisplayName("Дата продажи")]
        public DateTime? SoldDate { set; get; }
        [DisplayName("Стоимость")]
        public int Cost { set; get; }
        public Dictionary<string, (int, DateTime)> CarKits { set; get; }
    }
}
