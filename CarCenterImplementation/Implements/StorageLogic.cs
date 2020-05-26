using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using CarCenterImplementation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using CarCenterBusinessLogic.HelperModels;

namespace CarCenterImplementation.Implements
{
    public class StorageLogic : IStorageLogic
    {
        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Storages.Where(s => model == null || model.Id.HasValue && s.Id == model.Id.Value)
                    .ToList()
                    .Select(s => new StorageViewModel()
                    {
                        Id = s.Id,
                        StorageName = s.StorageName,
                        StoragedKits = context.StorageKits.Where(sk => sk.StorageId == s.Id)
                        .Include(sk => sk.Kit)
                        .ToDictionary(key => key.Kit.KitName, value => value.KitCount)
                    })
                    .ToList();
            }
        }

        public bool CheckCountKits(InstalledCarKit kit)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                int countStoragedKits = context.StorageKits
                    .Include(sk => sk.Kit)
                    .Where(sk => sk.Kit.KitName == kit.KitName)
                    .Sum(sk => sk.KitCount);
                return countStoragedKits >= kit.Count;
            }
        }

        public void RemoveKits(BuiltCarBindingModel model)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var carKit in model.CarKits)
                        {
                            if (!carKit.RemovedFromStorages)
                            {
                                int kitCount = carKit.Count;
                                var skList = context.StorageKits.Include(sk => sk.Kit).Where(sk => sk.Kit.KitName == carKit.KitName);
                                foreach (var sk in skList)
                                {
                                    if (sk.KitCount >= kitCount)
                                    {
                                        sk.KitCount -= kitCount;
                                        kitCount = 0;
                                        context.BuiltCarKits.Include(bck => bck.Kit)
                                            .FirstOrDefault(bck => bck.Kit.KitName == carKit.KitName).RemovedFromStorages = true;
                                        context.SaveChanges();
                                        break;
                                    }
                                    else
                                    {
                                        kitCount -= sk.KitCount;
                                        sk.KitCount = 0;
                                        context.SaveChanges();
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void AddKitToStorage(DepositKitBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.DepositKitDates.Add(new DepositKitDate()
                {
                    DepositDate = DateTime.Now,
                    KitId = model.KitId,
                    KitCount = model.KitCount,
                    StorageId = model.StorageId
                });
                var storagedKit = context.StorageKits
                    .FirstOrDefault(sk => sk.KitId == model.KitId && sk.StorageId == model.StorageId);
                if (storagedKit != null)
                    storagedKit.KitCount += model.KitCount;
                else
                    context.StorageKits.Add(new StorageKit()
                    {
                        KitId = model.KitId,
                        StorageId = model.StorageId,
                        KitCount = model.KitCount
                    });
                context.SaveChanges();
            }
        }
    }
}
