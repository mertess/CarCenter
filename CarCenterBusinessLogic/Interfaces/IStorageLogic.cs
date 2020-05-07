using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        void CreateOrUpdate(StorageBindingModel model);
        void Delete(StorageViewModel model);
        List<StorageViewModel> Read(StorageBindingModel model);
        //void AddKit();
        void RemoveKits(CarBindingModel model);
    }
}
