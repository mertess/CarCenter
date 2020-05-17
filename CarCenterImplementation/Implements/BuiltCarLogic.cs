using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using CarCenterImplementation.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                            car = new BuiltCar();
                            context.BuiltCars.Add(car);
                            context.SaveChanges();
                            foreach (var kit in model.CarKits)
                            {
                                context.CarKits.Add(new CarKit()
                                {
                                    KitId = context.Kits.FirstOrDefault(k => k.KitName == kit.Key).Id,
                                    BuiltCarId = context.BuiltCars.Count() - 1,
                                    KitCount = kit.Value,
                                    InstallationDate = DateTime.Now
                                });
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            car = context.BuiltCars.FirstOrDefault(bc => bc.Id == model.Id.Value);
                            //удаляем те, которых нет в модели
                            var kits = context.CarKits.Include(ck => ck.Kit).Where(ck => ck.BuiltCarId == model.Id);
                            foreach (var kit in kits)
                            {
                                if (!model.CarKits.ContainsKey(kit.Kit.KitName))
                                {
                                    context.CarKits.Remove(kit);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    kit.KitCount = model.CarKits[kit.Kit.KitName];
                                    model.CarKits.Remove(kit.Kit.KitName);
                                    context.SaveChanges();
                                }
                            }
                            //добавляем новые
                            foreach (var kit in model.CarKits)
                            {
                                context.CarKits.Add(new CarKit()
                                {
                                    BuiltCarId = model.Id.Value,
                                    KitId = context.Kits.FirstOrDefault(k => k.KitName == kit.Key).Id,
                                    KitCount = kit.Value,
                                    InstallationDate = DateTime.Now
                                });
                                context.SaveChanges();
                            }
                        }
                        car.SoldDate = model.SoldDate;
                        car.CarId = context.Cars.FirstOrDefault(c => c.CarName == model.CarName).Id;
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
                        .ToDictionary(key => key.Kit.KitName, Value => Value.KitCount)
                    })
                    .ToList();
            }
        }
    }
}
