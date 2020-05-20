using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using CarCenterBusinessLogic.HelperModels;
using CarCenterImplementation.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarCenterImplementation.Implements
{
    public class BuiltCarLogic : IBuiltCarLogic
    {
        public void CreateOrUpdate(BuiltCarBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext()) {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        BuiltCar car;
                        if (!model.Id.HasValue)
                        {
                            car = new BuiltCar() { CarId = context.Cars.FirstOrDefault(c => c.CarName == model.CarName).Id };
                            context.BuiltCars.Add(car);
                            context.SaveChanges();
                            var newBuiltCarId = context.BuiltCars.Include(bc => bc.Car)
                                .FirstOrDefault(bc => bc.Car.CarName == model.CarName).Id;
                            foreach (var kit in model.CarKits)
                            {
                                context.CarKits.Add(new CarKit()
                                {
                                    KitId = context.Kits.FirstOrDefault(k => k.KitName == kit.KitName).Id,
                                    BuiltCarId = newBuiltCarId,
                                    KitCount = kit.Count,
                                    InstallationDate = DateTime.Now
                                });
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            car = context.BuiltCars.FirstOrDefault(bc => bc.Id == model.Id.Value);
                            car.CarId = context.Cars.FirstOrDefault(c => c.CarName == model.CarName).Id;
                            //удаляем те, которых нет в модели
                            var kits = context.CarKits.Include(ck => ck.Kit).Where(ck => ck.BuiltCarId == model.Id);
                            foreach (var kit in kits)
                            {
                                var carkit = model.CarKits.FirstOrDefault(ck => ck.KitName == kit.Kit.KitName);
                                if (carkit == null)
                                {
                                    context.CarKits.Remove(kit);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    kit.KitCount = carkit.Count;
                                    model.CarKits.Remove(carkit);
                                    context.SaveChanges();
                                }
                            }
                            //добавляем новые
                            foreach (var kit in model.CarKits)
                            {
                                context.CarKits.Add(new CarKit()
                                {
                                    BuiltCarId = model.Id.Value,
                                    KitId = context.Kits.FirstOrDefault(k => k.KitName == kit.KitName).Id,
                                    KitCount = kit.Count,
                                    InstallationDate = DateTime.Now
                                });
                                context.SaveChanges();
                            }
                        }
                        car.SoldDate = model.SoldDate;
                        context.SaveChanges();
                        transaction.Commit();
                    }catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(BuiltCarViewModel model)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var car = context.BuiltCars.Include(bc => bc.CarKits).FirstOrDefault(bc => bc.Id == model.Id);
                        if (car == null)
                            throw new Exception("Такой машины нет!");
                        context.CarKits.RemoveRange(car.CarKits);
                        context.BuiltCars.Remove(car);
                        context.SaveChanges();
                        transaction.Commit();
                    }catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        //добавить расчет финальной цены, скорее всего лучше обновить модель, и хранить информацию в бд 
        public List<BuiltCarViewModel> Read(BuiltCarBindingModel model)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                return context.BuiltCars
                    .Include(bc => bc.Car)
                    .Where(bc => model == null || bc.Id == model.Id.Value && model.Id.HasValue)
                    .ToList()
                    .Select(bc => new BuiltCarViewModel()
                    {
                        Id = bc.Id,
                        CarName = bc.Car.CarName,
                        SoldDate = bc.SoldDate,
                        CarKits = context.CarKits.Include(ck => ck.Kit).Where(ck => ck.BuiltCarId == bc.Id)
                        .Select(ck => new InstalledCarKit()
                        {
                            KitName = ck.Kit.KitName,
                            Count = ck.KitCount,
                            InstallationDate = ck.InstallationDate
                        }).ToList()
                    })
                    .ToList();
            }
        }
    }
}
