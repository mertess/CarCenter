using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.HelperModels;
using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> Read(StorageBindingModel model);
        bool CheckCountKits(InstalledCarKit installedCarKit);
        void AddKitToStorage(DepositKitBindingModel model);
        void RemoveKits(BuiltCarBindingModel model);
    }
}
