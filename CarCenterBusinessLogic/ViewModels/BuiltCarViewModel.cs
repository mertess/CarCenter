﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterBusinessLogic.ViewModels
{
    public class BuiltCarViewModel
    {
        public int Id { set; get; }
        public string CarName { set; get; }
        public int FinalCost { set; get; }
        public DateTime? SoldDate { set; get; }
        public Dictionary<string, int> CarKits { set; get; }
    }
}