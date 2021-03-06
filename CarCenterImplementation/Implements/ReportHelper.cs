﻿using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using CarCenterImplementation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarCenterImplementation.Implements
{
    public class ReportHelper : IReportHelper
    {
        public List<CarKitViewModel> GetCarKits(KitReportPeriodBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.BuiltCarKits
                    .Where(ck => ck.InstallationDate >= model.DateFrom && ck.InstallationDate <= model.DateTo)
                    .Include(ck => ck.Kit)
                    .Select(ck => new CarKitViewModel()
                    {
                        KitId = ck.Kit.Id,
                        KitName = ck.Kit.KitName,
                        KitCost = ck.Kit.Cost,
                        KitCount = ck.KitCount,
                        CarName = context.BuiltCars.Include(bc => bc.Car)
                        .FirstOrDefault(bc => bc.Id == ck.BuiltCarId).Car.CarName,
                        InstallationDate = ck.InstallationDate
                    })
                    .ToList();
            }
        }

        public List<DepositKitViewModel> GetKitsDeposit(KitReportPeriodBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.DepositKitDates
                    .Where(dk => dk.DepositDate >= model.DateFrom && dk.DepositDate <= model.DateTo)
                    .Include(dk => dk.Kit)
                    .Select(dk => new DepositKitViewModel()
                    {
                        KitId = dk.KitId,
                        KitCost = dk.Kit.Cost,
                        KitName = dk.Kit.KitName,
                        KitCount = dk.KitCount,
                        DepositDate = dk.DepositDate
                    })
                    .ToList();
            }
        }
    }
}
