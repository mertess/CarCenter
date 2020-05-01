using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class KitViewModel
    {
        public int Id { set; get; }
        [DisplayName("Комплектация")]
        public string KitName { set; get; }
    }
}
