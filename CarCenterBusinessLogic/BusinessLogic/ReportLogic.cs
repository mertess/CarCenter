using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.BusinessLogic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using CarCenterBusinessLogic.BindingModels;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly ICarLogic carLogic;
        private readonly IStorageLogic storageLogic;

        public ReportLogic(ICarLogic carLogic, IStorageLogic storageLogic)
        {
            this.carLogic = carLogic;
            this.storageLogic = storageLogic;
        }

        //сделать выборку 
        public List<ReportActionKitsViewModel> GetReportActionKits(KitReportPeriodBindingModel model)
        {
            var result = new List<ReportActionKitsViewModel>();
            return result;
        }
    }
}
