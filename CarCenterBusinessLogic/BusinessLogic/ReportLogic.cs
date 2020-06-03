using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.BusinessLogic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using CarCenterBusinessLogic.BindingModels;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using CarCenterBusinessLogic.HelperModels;
using System.Diagnostics;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IReportHelper reportHelper;
        private readonly IKitLogic kitLogic;
        private readonly IBuiltCarLogic builtCarLogic;
        private readonly ICarLogic carLogic;
        private readonly string TmpWordFilePath = $@"{AppContext.BaseDirectory}\TmpFiles\WordReport.docx";
        private readonly string TmpExcelFilePath = $@"{AppContext.BaseDirectory}\TmpFiles\ExcelReport.xlsx";
        private readonly string TmpPdfFilePath = $@"{AppContext.BaseDirectory}\TmpFiles\PdfReport.PDF";

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

        public void SendWordReport(string Email)
        {
            try
            {
                WordReporter.CreateDoc(new WordInfo()
                {
                    FilePath = TmpWordFilePath,
                    Title = "Автоцентр \"Корыто\"",
                    Body = "Проданные машины с установленными комплектациями:",
                    ReportSoldCars = GetReportSoldCars()
                });
                MailService.SendDocumentAsync(new MailSendInfo()
                {
                    FilePath = TmpWordFilePath,
                    Title = "Автоцентр \"Корыто\" Отчет Word",
                    Body = "Проданные машины с установленными комплектациями",
                    Email = Email
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SendExcelReport(string Email)
        {
            try
            {
                ExcelReporter.CreateDoc(new ExcelInfo()
                {
                    FilePath = TmpExcelFilePath,
                    Title = "Автоцентр \"Корыто\"",
                    Body = "Проданные машины с установленными комплектациями:",
                    ReportSoldCars = GetReportSoldCars()
                });
                MailService.SendDocumentAsync(new MailSendInfo()
                {
                    FilePath = TmpExcelFilePath,
                    Title = "Автоцентр \"Корыто\" Отчет Excel",
                    Body = "Проданные машины с установленными комплектациями",
                    Email = Email
                });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void SendPdfReport(string Email, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                PdfReporter.CreateDoc(new PdfInfo()
                {
                    FilePath = TmpPdfFilePath,
                    DateFrom = DateFrom.ToShortDateString(),
                    DateTo = DateTo.ToShortDateString(),
                    Title = "Автоцентр \"Корыто\"",
                    Body = "Отчет по движению комплектаций",
                    ReportActionKits = GetReportActionKits(new KitReportPeriodBindingModel()
                    {
                        DateFrom = DateFrom,
                        DateTo = DateTo,
                    })
                });
                MailService.SendDocumentAsync(new MailSendInfo()
                {
                    FilePath = TmpPdfFilePath,
                    Title = "Автоцентр \"Корыто\" Отчет Pdf",
                    Body = "Отчет по движению комплектаций",
                    Email = Email
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
