using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.BusinessLogic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using CarCenterBusinessLogic.BindingModels;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IReportHelper reportHelper;
        private readonly IKitLogic kitLogic;
        private readonly IBuiltCarLogic builtCarLogic;
        private readonly ICarLogic carLogic;

        public ReportLogic(IReportHelper reportHelper, IKitLogic kitLogic, IBuiltCarLogic builtCarLogic, ICarLogic carLogic)
        {
            this.reportHelper = reportHelper;
            this.kitLogic = kitLogic;
            this.builtCarLogic = builtCarLogic;
            this.carLogic = carLogic;
        }

        
        public List<ReportActionKitsViewModel> GetReportActionKits(KitReportPeriodBindingModel model)
        {
            var result = new List<ReportActionKitsViewModel>();
            foreach(var dk in reportHelper.GetKitsDeposit(model))
            {
                result.Add(new ReportActionKitsViewModel()
                {
                    Action = "Пришла",
                    DateAction = dk.DepositDate,
                    KitName = dk.KitName,
                    KitCost = dk.KitCost,
                    KitCount = dk.KitCount
                });
            }
            foreach (var ck in reportHelper.GetCarKits(model))
            {
                result.Add(new ReportActionKitsViewModel()
                {
                    Action = "Ушла " + ck.CarName,
                    DateAction = ck.InstallationDate,
                    KitName = ck.KitName,
                    KitCount = ck.KitCount,
                    KitCost = ck.KitCost
                });
            }
            return result;
        }

        public List<ReportSoldCarViewModel> GetReportSoldCars()
        {
            var result = new List<ReportSoldCarViewModel>();
            var kits = kitLogic.Read(null);
            var cars = carLogic.Read(null);
            foreach(var c in builtCarLogic.Read(null).Where(c => c.SoldDate.HasValue))
            {
                result.Add(new ReportSoldCarViewModel()
                {
                    CarName = c.CarName,
                    CarCost = cars.FirstOrDefault(cl => cl.CarName == c.CarName).Cost,
                    SoldDate = c.SoldDate.Value,
                    CarKits = c.CarKits.ToDictionary(
                        key => key.KitName,
                        value => (kits.FirstOrDefault(k => k.KitName == value.KitName).KitCost, value.Count))
                });
            }
            return result;
        }
    }
}
