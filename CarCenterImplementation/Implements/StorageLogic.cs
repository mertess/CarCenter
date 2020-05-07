using CarCenterBusinessLogic.Interfaces;
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
    public class StorageLogic : IStorageLogic
    {
        public void CreateOrUpdate(StorageBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Storage storage;
                if (!model.Id.HasValue)
                {
                    storage = context.Storages.FirstOrDefault(s => s.StorageName == model.StorageName);
                    if (storage != null)
                        throw new Exception("Такое хранилище уже есть!");
                    storage = new Storage();
                    context.Storages.Add(storage);
                }
                else
                {
                    storage = context.Storages.FirstOrDefault(s => s.Id == model.Id);
                }
                storage.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }

        public void Delete(StorageViewModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var storage = context.Storages.FirstOrDefault(s => s.Id == model.Id);
                if (storage == null)
                    throw new Exception("Такого хранилища нет!");
                context.Storages.Remove(storage);
                context.SaveChanges();
            }
        }

        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Storages.Where(s => model == null || model.Id.HasValue && s.Id == model.Id.Value)
                    .Select(s => new StorageViewModel()
                    {
                        Id = s.Id,
                        StorageName = s.StorageName
                    })
                    .ToList();
            }
        }

        public void RemoveKits(CarBindingModel model)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var carKit in model.CarKits)
                        {
                            int kitCount = carKit.Value.Item1;
                            var skList = context.StorageKits.Include(sk => sk.Kit).Where(sk => sk.Kit.KitName == carKit.Key);
                            foreach (var sk in skList)
                            {
                                if(sk.KitCount >= kitCount)
                                {
                                    sk.KitCount -= kitCount;
                                    kitCount = 0;
                                    context.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    sk.KitCount = 0;
                                    kitCount -= sk.KitCount;
                                    context.SaveChanges();
                                }
                            }
                            if (kitCount > 0)
                                throw new Exception("Не хватает комплектаций на складах!");    
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
    }
}
