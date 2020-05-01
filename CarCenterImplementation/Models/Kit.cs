using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarCenterImplementation.Models
{
    public class Kit
    {
        public int Id { set; get; }
        [Required]
        public string KitName { set; get; }
        public int Cost { set; get; }
        [ForeignKey("KitId")]
        public virtual List<CarKit> CarKits { set; get; }
        [ForeignKey("KitId")]
        public virtual List<StorageKit> StorageKits { set; get; }
        [ForeignKey("KitId")]
        public virtual List<DepositKitDate> DepositKitDates { set; get; }
    }
}
