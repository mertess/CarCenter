using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class StorageKitViewModel
    {
        [DisplayName("Комплектация")]
        public string KitName { set; get; }
        [DisplayName("Количество")]
        public int KitCount { set; get; }
    }
}
