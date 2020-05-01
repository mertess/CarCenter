using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.Interfaces
{
    public interface ICarLogic
    {
        void CreateOrUpdate(CarBindingModel model);
        void Delete(CarViewModel model);
        List<CarViewModel> Read(CarBindingModel model);
    }
}
