using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CarCenterImplementation.Models;

namespace CarCenterImplementation
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BIJFUOL;
                Initial Catalog=CarCenterDataBase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<CarKit> CarKits { set; get; }
        public virtual DbSet<Kit> Kits { set; get; }
        public virtual DbSet<DepositKitDate> DepositKitDates { set; get; }
        public virtual DbSet<Storage> Storages { set; get; }
        public virtual DbSet<StorageKit> StorageKits { set; get; }
        public virtual DbSet<BuiltCar> BuiltCars { set; get; }
    }
}
