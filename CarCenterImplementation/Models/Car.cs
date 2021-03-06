﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarCenterImplementation.Models
{
    public class Car
    {
        public int Id { set; get; }
        [Required]
        public string CarName { set; get; }
        [Required]
        public decimal Cost { set; get; }
        [ForeignKey("CarId")]
        public virtual List<BuiltCar> BuiltCars { set; get; }
    }
}
