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
    public class CarLogic : ICarLogic
    {
        public void CreateOrUpdate(CarBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Car car;
                if (!model.Id.HasValue)
                {
                    car = context.Cars.FirstOrDefault(c => c.CarName == model.CarName);
                    if (car != null)
                        throw new Exception("Такая машина уже есть!");
                    car = new Car();
                    context.Cars.Add(car);
                }
                else
                {
                    car = context.Cars.FirstOrDefault(c => c.Id == model.Id.Value);
                }
                car.CarName = model.CarName;
                car.Cost = model.Cost;
                context.SaveChanges();
            }
        }

        public void Delete(CarViewModel model)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                var car = context.Cars.FirstOrDefault(c => c.Id == model.Id);
                if (car == null)
                    throw new Exception("Такой машины нет!");
                context.Cars.Remove(car);
                context.SaveChanges();
            }
        }

        public List<CarViewModel> Read(CarBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Cars.Where(c => model == null || model.Id.HasValue && c.Id == model.Id.Value)
                    .ToList()
                    .Select(c => new CarViewModel()
                    {
                        Id = c.Id,
                        CarName = c.CarName,
                        Cost = c.Cost
                    })
                    .ToList();
            }
        }
    }
}
