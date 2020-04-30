using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarCenterImplementation.Models
{
    public class Car
    {
        public int Id { set; get; }
        [Required]
        public string CarName { set; get; }
        public DateTime SoldDate { set; get; }
        [ForeignKey("CarId")]
        public virtual List<CarKit> CarKits { set; get; }
    }
}
