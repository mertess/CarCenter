using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.Interfaces
{
    public interface IKitLogic
    {
        void CreateOrUpdate(KitBindingModel model);
        void Delete(KitViewModel model);
        List<KitViewModel> Read(KitBindingModel model);
    }
}
