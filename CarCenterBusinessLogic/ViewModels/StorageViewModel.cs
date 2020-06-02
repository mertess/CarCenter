using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class StorageViewModel
    {
        public int Id { set; get; }
        public string StorageName { set; get; }
        public Dictionary<string, int> StoragedKits { set; get; }
    }
}
