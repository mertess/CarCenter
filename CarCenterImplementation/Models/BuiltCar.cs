using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarCenterImplementation.Models
{
    public class BuiltCar
    {
        public int Id { set; get; }
        public DateTime? SoldDate { set; get; }
        public int CarId { set; get; }
        [ForeignKey("BuiltCarId")]
        public virtual List<CarKit> CarKits { set; get; }
        public virtual Car Car { set; get; }
    }
}
