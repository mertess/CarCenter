using CarCenterBusinessLogic.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarCenterImplementation.Implements
{
    public class BackUpLogic : BackUpAbstractLogic
    {
        protected override Assembly GetAssembly() => typeof(BackUpLogic).Assembly;

        protected override List<PropertyInfo> GetSetsOfModels()
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                return context.GetType().GetProperties()
                    .Where(p => p.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
            }
        }

        protected override List<T> GetSetValues<T>()
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}
