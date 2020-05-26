using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CarCenterImplementation.Models;

namespace CarCenterImplementation
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            //создает БД при её отсутствии
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BIJFUOL;
                Initial Catalog=CarCenterDataBase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>().HasData(new Storage[]
            {
                new Storage(){ Id = 1, StorageName = "Хранилище 1" },
                new Storage(){ Id = 2, StorageName = "Хранилище 2" },
                new Storage(){ Id = 3, StorageName = "Хранилище 3" },
                new Storage(){ Id = 4, StorageName = "Хранилище 4" }
            });
        }

        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<BuiltCarKit> BuiltCarKits { set; get; }
        public virtual DbSet<Kit> Kits { set; get; }
        public virtual DbSet<DepositKitDate> DepositKitDates { set; get; }
        public virtual DbSet<Storage> Storages { set; get; }
        public virtual DbSet<StorageKit> StorageKits { set; get; }
        public virtual DbSet<BuiltCar> BuiltCars { set; get; }
    }
}
