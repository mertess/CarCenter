using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarCenterImplementation.Models
{
    public class CarKit
    {
        public int Id { set; get; }
        public DateTime InstallationDate { set; get; }
        public int KitCount { set; get; }
        public int KitId { set; get; }
        public int BuiltCarId { set; get; }
        public virtual Kit Kit { set; get; }
        public virtual BuiltCar Car { set; get; }
    }
}
