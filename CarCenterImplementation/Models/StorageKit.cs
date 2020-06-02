﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarCenterImplementation.Models
{
    public class StorageKit
    {
        public int Id { set; get; }
        public int KitCount { set; get; }
        public int KitId { set; get; }
        public int StorageId { set; get; }
        public virtual Kit Kit { set; get; }
        public virtual Storage Storage { set; get; }
    }
}
