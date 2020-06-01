using CarCenterBusinessLogic.BusinessLogic;
using CarCenterImplementation.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        protected override void InsertSetValues<T>(List<T> records)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                context.Set<T>().Clear();
                context.Set<T>().AddRange(records);
                var strOn = $"SET IDENTITY_INSERT dbo.{typeof(T).Name + "s"} ON";
                var strOff = $"SET IDENTITY_INSERT dbo.{typeof(T).Name + "s"} OFF";
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand(strOn);
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand(strOff);
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
        }
    }
}
