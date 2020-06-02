using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarCenterImplementation.Extensions
{
    public static class DbSetExtension
    {
        public static void Clear<T>(this DbSet<T> set) where T: class => set.RemoveRange(set);
    }
}
