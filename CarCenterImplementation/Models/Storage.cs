using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarCenterImplementation.Models
{
    public class Storage
    {
        public int Id { set; get; }
        [Required]
        public string StorageName { set; get; }
        [ForeignKey("StorageId")]
        public virtual List<StorageKit> StorageKits { set; get; }
        [ForeignKey("StorageId")]
        public virtual List<DepositKitDate> DepositKitDates { set; get; }
    }
}
