using CarCenterBusinessLogic.Interfaces;
using CarCenterBusinessLogic.BindingModels;
using CarCenterBusinessLogic.ViewModels;
using CarCenterImplementation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CarCenterImplementation.Implements
{
    public class KitLogic : IKitLogic
    {
        public void CreateOrUpdate(KitBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Kit kit;
                if (!model.Id.HasValue)
                {
                    kit = context.Kits.FirstOrDefault(k => k.KitName == model.KitName);
                    if (kit != null)
                        throw new Exception("Такая комплектация уже есть!");
                    kit = new Kit();
                    context.Kits.Add(kit);
                }
                else
                {
                    kit = context.Kits.FirstOrDefault(k => k.Id == model.Id.Value);
                }
                kit.KitName = model.KitName;
                kit.Cost = model.Cost;
                context.SaveChanges();
            }
        }

        public void Delete(KitViewModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var kit = context.Kits.FirstOrDefault(k => k.Id == model.Id);
                if (kit == null)
                    throw new Exception("Такой комплектации нет!");
                context.Kits.Remove(kit);
                context.SaveChanges();
            }
        }

        public List<KitViewModel> Read(KitBindingModel model)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Kits.Where(k => model == null || model.Id.HasValue && k.Id == model.Id.Value)
                    .Select(k => new KitViewModel()
                    {
                        Id = k.Id,
                        KitName = k.KitName,
                        KitCost = k.Cost
                    })
                    .ToList();
            }
        }
    }
}
