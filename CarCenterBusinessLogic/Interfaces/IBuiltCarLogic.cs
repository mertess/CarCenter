using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.Interfaces
{
    public interface IBuiltCarLogic
    {
        void CreateOrUpdate(BuiltCarBindingModel model);
        void Delete(BuiltCarViewModel model);
        List<BuiltCarViewModel> Read(BuiltCarBindingModel model);
    }
}
