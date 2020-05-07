using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.Interfaces
{
    public interface IReportHelper
    {
        List<CarKitViewModel> GetCarKits(KitReportPeriodBindingModel model);
        List<DepositKitViewModel> GetKitsDeposit(KitReportPeriodBindingModel model);
    }
}
